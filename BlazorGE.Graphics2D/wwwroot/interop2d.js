export function clearRect(x, y, width, height) {
    window.blazorGECanvas2DContext.clearRect(x, y, width, height);
}

export function drawBatch(batch) {
    for (var i = 0; i < batch.length; i++) {
        var functionName = batch[i].slice(0, 1)[0];
        var parameters = batch[i].slice(1, batch[i].length);

        switch (functionName) {
            case 'clearRect': clearRect.apply(null, parameters); //clearRect(parameters[0], parameters[1], parameters[2], parameters[3]);
                break;

            case 'drawFilledPolygon': drawFilledPolygon.apply(null, parameters); //drawFilledPolygon(parameters[0], parameters[1], parameters[2]);
                break;

            case 'drawFilledRectangle': drawFilledRectangle.apply(null, parameters); //drawFilledRectangle(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]);
                break;

            case 'drawImage': drawImage.apply(null, parameters); //drawImage(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6], parameters[7], parameters[8]);
                break;

            case 'drawQuadrilateral': drawQuadrilateral.apply(null, parameters); //drawQuadrilateral(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6], parameters[7], parameters[8]);
                break;

            case 'drawText': drawText.apply(null, parameters); //drawText(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5]);
                break;

            case 'drawTrapezium': drawTrapezium.apply(null, parameters); //drawTrapezium(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6]);
                break;
                                
            default: break;
        }
    }
}

export function drawFilledPolygon(coordinates, fillColor, strokeColor) {
    if (coordinates.length <= 0) return;

    window.blazorGECanvas2DContext.beginPath();
    window.blazorGECanvas2DContext.moveTo(coordinates[0][0], coordinates[0][1]);

    for (var i = 1; i < coordinates.length; i++) {
        window.blazorGECanvas2DContext.lineTo(coordinates[i][0], coordinates[i][1]);
    }

    if (strokeColor != null && strokeColor != undefined) {
        window.blazorGECanvas2DContext.strokeStyle = strokeColor;
    }
    
    window.blazorGECanvas2DContext.closePath();
    window.blazorGECanvas2DContext.fillStyle = fillColor;
    window.blazorGECanvas2DContext.fill();
}

export function drawFilledRectangle(colour, x, y, width, height) {
    window.blazorGECanvas2DContext.fillStyle = colour;
    window.blazorGECanvas2DContext.fillRect(x, y, width, height);
}

export function drawImage(reference, sx, sy, sw, sh, dx, dy, dw, dh) {
    window.blazorGECanvas2DContext.drawImage(reference, sx, sy, sw, sh, dx, dy, dw, dh);
}

export function drawQuadrilateral(fillColour, x1, y1, x2, y2, x3, y3, x4, y4) {
    window.blazorGECanvas2DContext.beginPath();
    window.blazorGECanvas2DContext.moveTo(x1, y1);
    window.blazorGECanvas2DContext.lineTo(x2, y2);
    window.blazorGECanvas2DContext.lineTo(x3, y3);
    window.blazorGECanvas2DContext.lineTo(x4, y4);
    window.blazorGECanvas2DContext.closePath();

    window.blazorGECanvas2DContext.fillStyle = fillColour;
    window.blazorGECanvas2DContext.fill();
}

export function drawText(text, x, y, font, colour, isFilled) {    
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

export function drawTrapezium(fillColour, x1, y1, w1, x2, y2, w2) {    
    window.blazorGECanvas2DContext.beginPath();
    window.blazorGECanvas2DContext.moveTo(x1 - w1, y1);
    window.blazorGECanvas2DContext.lineTo(x2 - w2, y2);
    window.blazorGECanvas2DContext.lineTo(x2 + w2, y2);
    window.blazorGECanvas2DContext.lineTo(x1 + w1, y1);
    window.blazorGECanvas2DContext.closePath();

    window.blazorGECanvas2DContext.fillStyle = fillColour;
    window.blazorGECanvas2DContext.fill();
}

export function initialiseCanvas2D(instance, canvasReference) {
    window.blazorGEGraphicsInstance = instance;
    window.blazorGECanvasElement = canvasReference;
    window.blazorGECanvas2DContext = window.blazorGECanvasElement.getContext('2d');
        
    window.addEventListener('resize', onResizeCanvas);
    onResizeCanvas();
}

function onResizeCanvas() {
    window.blazorGECanvasElement.style.width = '100%';
    window.blazorGECanvasElement.style.height = '100%';
    
    const width = window.blazorGECanvasElement.clientWidth;
    const height = window.blazorGECanvasElement.clientHeight;

    if (window.blazorGECanvasElement.width !== width || window.blazorGECanvasElement.height !== height) {
        window.blazorGECanvasElement.width = width;
        window.blazorGECanvasElement.height = height;        
    }

    window.blazorGEGraphicsInstance.invokeMethodAsync('OnResizeCanvas', window.blazorGECanvasElement.width, window.blazorGECanvasElement.height);
}