export function clearRect(x, y, width, height) {
    window.blazorGECanvas2DContext.clearRect(x, y, width, height);
}

export function drawImage(reference, sx, sy, sw, sh, dx, dy, dw, dh) {
    window.blazorGECanvas2DContext.drawImage(reference, sx, sy, sw, sh, dx, dy, dw, dh);
}

export function drawText(text, x, y, colour, font, isFilled) {    
    window.blazorGECanvas2DContext.font = font;

    if (isFilled) {
        window.blazorGECanvas2DContext.fillStyle = colour;
        window.blazorGECanvas2DContext.fillText(text, x, y);
    }
    else {
        window.blazorGECanvas2DContext.strokeStyle = colour;
        window.blazorGECanvas2DContext.strokeText(text, x, y);
    }
}

export function initialiseCanvas2D(instance) {
    window.blazorGEGraphicsInstance = instance;
    window.blazorGECanvasElement = document.getElementById('blazorge-canvas');
    window.blazorGECanvas2DContext = window.blazorGECanvasElement.getContext('2d');

    window.addEventListener('resize', onResizeCanvas);
    onResizeCanvas();    
}

function onResizeCanvas() {
    var canvas = window.blazorGECanvasElement;
    canvas.style.width = '100%';
    canvas.style.height = '100%';
    canvas.width = canvas.offsetWidth;
    canvas.height = canvas.offsetHeight;

    window.blazorGEGraphicsInstance.invokeMethodAsync('OnResizeCanvas', canvas.width, canvas.height);
}              