window.CropImage = (source) => {
    // const originalImage = new Image();
    // originalImage.src = source;
    console.log("1");
    // const canvas = document.createElement('canvas');
    const canvas = document.getElementById('canvas');
    console.log("2");
    //document.getElementById('canvas' + id);
    const ctx = canvas.getContext('2d');

    ctx.putImageData(source,100,100);
    
    const originalWidth = originalImage.naturalWidth;
    const originalHeight = originalImage.naturalHeight;

    let sx = 0;
    let sy = 0;
    let sWidth = originalWidth;
    let sHeight = originalHeight;
    
    if (originalWidth > originalHeight)
    {
        sx = (originalWidth - originalHeight) / 2;
        sWidth = originalWidth - 2 * sx;
    }
    else
    {
        sy = (originalHeight - originalWidth)  / 2;
        sHeight = originalHeight - 2 * sy;
    }

    Math.min(originalWidth, originalHeight);
    
    const aspectRatio = originalWidth/originalHeight;
    const xOffset = (originalWidth - 800) / 2;
    const yOffset = (originalHeight - 800) / 2;

    console.log(originalWidth)
    console.log(originalHeight)
    console.log(xOffset)
    console.log(yOffset)
    console.log(sWidth)
    console.log(sHeight)
    ctx.drawImage(originalImage, sx, sy, sWidth, sHeight, 0, 0, 800, 800);
    const newImage = ctx.toDataURL();
    console.log(newImage);
    return newImage;
}
