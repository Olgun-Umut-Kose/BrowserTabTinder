function muteAutoplayVideos() {
  const videos = document.querySelectorAll("video");
  videos.forEach(v => {
      v.pause();
  });
}


function startOnce() {
  if (document.body) {
    muteAutoplayVideos();
  } else {
    
    document.addEventListener("DOMContentLoaded", muteAutoplayVideos);
  }
}

startOnce();
