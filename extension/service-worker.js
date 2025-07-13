


chrome.tabs.query({}, (tabs) => {

    console.log(tabs)

    

});

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
}