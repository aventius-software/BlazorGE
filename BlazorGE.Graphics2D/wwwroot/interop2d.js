let blazorGEGraphicsInstance = null;
let blazorGECanvasElement = null;
let blazorGECanvas2DContext = null;

export function clearRect(x, y, width, height) {
    blazorGECanvas2DContext.clearRect(x, y, width, height);
}

export function drawBatch(batch) {
    for (var i = 0; i < batch.length; i++) {
        var functionName = batch[i].slice(0, 1)[0];
        var parameters = batch[i].slice(1, batch[i].length);

        switch (functionName) {
            case 'clearRect': clearRect.apply(null, parameters);
                break;

            case 'drawFilledPolygon': drawFilledPolygon.apply(null, parameters);
                break;

            case 'drawFilledRectangle': drawFilledRectangle.apply(null, parameters);
                break;

            case 'drawImage': drawImage.apply(null, parameters);
                break;

            case 'drawQuadrilateral': drawQuadrilateral.apply(null, parameters);
                break;

            case 'drawText': drawText.apply(null, parameters);
                break;

            case 'drawTrapezium': drawTrapezium.apply(null, parameters);
                break;
                                
            default: break;
        }
    }
}

export function drawFilledPolygon(coordinates, fillColor, strokeColor) {
    if (coordinates.length <= 0) return;

    blazorGECanvas2DContext.beginPath();
    blazorGECanvas2DContext.moveTo(coordinates[0][0], coordinates[0][1]);

    for (var i = 1; i < coordinates.length; i++) {
        blazorGECanvas2DContext.lineTo(coordinates[i][0], coordinates[i][1]);
    }

    if (strokeColor != null && strokeColor != undefined) {
        blazorGECanvas2DContext.strokeStyle = strokeColor;
    }
    
    blazorGECanvas2DContext.closePath();
    blazorGECanvas2DContext.fillStyle = fillColor;
    blazorGECanvas2DContext.fill();
}

export function drawFilledRectangle(colour, x, y, width, height) {
    blazorGECanvas2DContext.fillStyle = colour;
    blazorGECanvas2DContext.fillRect(x, y, width, height);
}

export function drawImage(reference, sx, sy, sw, sh, dx, dy, dw, dh) {
    blazorGECanvas2DContext.drawImage(reference, sx, sy, sw, sh, dx, dy, dw, dh);
}

export function drawQuadrilateral(fillColour, x1, y1, x2, y2, x3, y3, x4, y4) {
    blazorGECanvas2DContext.beginPath();
    blazorGECanvas2DContext.moveTo(x1, y1);
    blazorGECanvas2DContext.lineTo(x2, y2);
    blazorGECanvas2DContext.lineTo(x3, y3);
    blazorGECanvas2DContext.lineTo(x4, y4);    
    blazorGECanvas2DContext.fillStyle = fillColour;
    blazorGECanvas2DContext.fill();
}

export function drawText(text, x, y, font, colour, isFilled) {    
    blazorGECanvas2DContext.font = font;

    if (isFilled) {
        blazorGECanvas2DContext.fillStyle = colour;
        blazorGECanvas2DContext.fillText(text, x, y);
    }
    else {
        blazorGECanvas2DContext.strokeStyle = colour;
        blazorGECanvas2DContext.strokeText(text, x, y);
    }
}

export function drawTrapezium(fillColour, x1, y1, w1, x2, y2, w2) {    
    blazorGECanvas2DContext.beginPath();
    blazorGECanvas2DContext.moveTo(x1 - w1, y1);
    blazorGECanvas2DContext.lineTo(x2 - w2, y2);
    blazorGECanvas2DContext.lineTo(x2 + w2, y2);
    blazorGECanvas2DContext.lineTo(x1 + w1, y1);    
    blazorGECanvas2DContext.fillStyle = fillColour;
    blazorGECanvas2DContext.fill();
}

export function initialiseCanvas2D(instance, canvasReference) {
    blazorGEGraphicsInstance = instance;
    blazorGECanvasElement = canvasReference;
    blazorGECanvas2DContext = blazorGECanvasElement.getContext('2d');
        
    window.addEventListener('resize', onResizeCanvas);
    onResizeCanvas();

    window.blazorGEFunctions = {
        drawBatch: unmarshalledDrawBatch
    }
}

function onResizeCanvas() {
    blazorGECanvasElement.style.width = '100%';
    blazorGECanvasElement.style.height = '100%';
    
    const width = blazorGECanvasElement.clientWidth;
    const height = blazorGECanvasElement.clientHeight;

    if (blazorGECanvasElement.width !== width || blazorGECanvasElement.height !== height) {
        blazorGECanvasElement.width = width;
        blazorGECanvasElement.height = height;        
    }

    blazorGEGraphicsInstance.invokeMethodAsync('OnResizeCanvas', blazorGECanvasElement.width, blazorGECanvasElement.height);
}

function unmarshalledDrawBatch(payload) {
    // See https://github.com/mono/mono/blob/main/sdks/wasm/src/binding_support.js
    drawBatch(BINDING.mono_array_to_js_array(payload));

    // and...
    return 0;
}
