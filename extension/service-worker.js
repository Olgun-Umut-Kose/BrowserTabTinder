const socket = new WebSocket("ws://localhost:9988/ws/");

let currentTabIndex = -1;



chrome.tabs.query({}, async (tabs) => {
  
  
  socket.onmessage = async (event) => {
    const eventobj = JSON.parse(event.data);
    setTimeout(async () => {
      switch (eventobj.command.toLowerCase()) {
        case "next":
          if (tabs.length - 1 == currentTabIndex) {
            socket.send({
              command: "end",
              message: null,
            });
            break;
          }
          while (true) {
            try {
              await sendTabData(tabs[currentTabIndex +1]);
              break;
            } catch (error) {
              // for "chrome://" url 
            }
            finally{
              
              currentTabIndex++;
            }
          }
          break;
        case "reload":
          sendTabData(tabs[currentTabIndex]);
          break;
        default:
          break;
      }
    }, 600);
  };
  console.log(tabs);
  takePicture(2144055059, 2144055051);
  //moveWindowToTop(2144055051)
});

async function sendTabData(tab) {
  console.log("currentTab", tab);
 try {
  const imageb64 = await captureWhenReady(tab.id, tab.windowId);
  socket.send(
    JSON.stringify({
      command: "tabdata",
      message: {
        id: tab.id,
        windowid: tab.windowId,
        title: tab.title,
        url: tab.url,
        imageb64
      },
    })
  );
 } catch (error) {
  throw error;
 }
  
  
}

async function moveWindowToTop(windowid) {
  const uwindow = await new Promise((resolve, reject) => {
    chrome.windows.update(windowid, { focused: true }, async (uwindow) => {
      if (chrome.runtime.lastError) {
        reject(chrome.runtime.lastError);
      } else {
        resolve(uwindow);
      }
    });
  });
}



async function captureWhenReady(tabId, windowId) {
  const window = await chrome.windows.get(windowId);
  const originalState = window.state;

  if (originalState === "minimized") {
    await chrome.windows.update(windowId, { focused: true });
  }
  await chrome.tabs.update(tabId, { active: true, highlighted: true });

  await waitTabReady(tabId);

  return new Promise((resolve, reject) => {
    chrome.debugger.attach({ tabId }, "1.3", () => {
      if (chrome.runtime.lastError) {
        return reject(chrome.runtime.lastError.message);
      }

      chrome.debugger.sendCommand(
        { tabId },
        "Page.captureScreenshot",
        { format: "png", quality: 90 },
        async (result) => {
          chrome.debugger.detach({ tabId });
          if (chrome.runtime.lastError) {
            return reject(chrome.runtime.lastError.message);
          }

          
          resolve("data:image/png;base64," + result.data); // base64 png
        }
      );
    });
  });
}

async function waitTabReady(tabId) {
  return new Promise((resolve) => {
    chrome.tabs.get(tabId, async (tab) => {
      if (tab.status === "complete") {
        
        resolve();
      } else if (tab.status === "unloaded") {
        chrome.tabs.reload(tabId, () => {
          setTimeout(() => {
            waitTabReady(tabId).then(() => resolve());
          }, 1000);
        });
      } else {
        function listener(updatedTabId, info) {
          if (updatedTabId === tabId && info.status === "complete") {
            chrome.tabs.onUpdated.removeListener(listener);
            resolve();
          }
        }
        chrome.tabs.onUpdated.addListener(listener);
      }
    });
  });
}

