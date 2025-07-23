


chrome.tabs.query({}, (tabs) => {

    console.log(tabs)
    takePicture(2144055059,2144055051)
    //moveWindowToTop(2144055051)
    

});

async function moveWindowToTop(windowid) {
  

  const uwindow = await new Promise((resolve, reject) => {
chrome.windows.update(windowid,{focused:true},async (uwindow) => {

        
    if (chrome.runtime.lastError) {
          reject(chrome.runtime.lastError);
        } else {
          resolve(utab);
        }

        

    })
})
}

async function takePicture(tabid,windowid) {
    const utab = await new Promise((resolve, reject) => {
chrome.tabs.update(tabid,{active: true,highlighted:true},async (utab) => {

        
    if (chrome.runtime.lastError) {
          reject(chrome.runtime.lastError);
        } else {
          resolve(utab);
        }

        

    })
})

console.log(utab)

    await new Promise(r => setTimeout(r, 500));

    const base64Url = await new Promise((resolve, reject) => {
      chrome.tabs.captureVisibleTab(windowid, { format: 'png', quality: 90 }, (screenshotUrl) => {
        if (chrome.runtime.lastError) {
          reject(chrome.runtime.lastError);
        } else if (!screenshotUrl) {
          reject(new Error("SSUrl is undefined"));
        } else {
          resolve(screenshotUrl);
        }
      });
    });

    console.log(base64Url)
}