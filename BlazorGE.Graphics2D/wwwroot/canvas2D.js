var canvasContext = {};
var canvasElement = {};
//var exports = {};
//var lastFrameTime = 0;
var componentReference = {};
//var initialised = false;
//var animationRequest = -1;

export async function initialiseModule(assemblyName, componentName, canvasID) {    
    // Get the assembly exports
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    var exports = await getAssemblyExports(assemblyName + ".dll");    

    // Save a reference to the Blazor component
    componentReference = exports.BlazorGE.Graphics2D.Components.Canvas2D;//[componentName];

    // References to the canvas
    canvasElement = document.getElementById(canvasID);
    canvasContext = canvasElement.getContext("2d");

    // Catch canvas resize events
    window.addEventListener("resize", onResizeCanvas);
    onResizeCanvas();
}

export function clearRect(x, y, width, height) {
    canvasContext.clearRect(x, y, width, height);
}

export function drawFilledRectangle(colour, x, y, width, height) {
    canvasContext.fillStyle = colour;
    canvasContext.fillRect(x, y, width, height);
}

export function drawFilledPolygon(coordinates, fillColor, strokeColor) {    
    if (coordinates.length <= 0) return;

    canvasContext.beginPath();
    canvasContext.moveTo(coordinates[0][0], coordinates[0][1]);

    for (var i = 1; i < coordinates.length; i++) {
        canvasContext.lineTo(coordinates[i][0], coordinates[i][1]);
    }

    if (strokeColor != null && strokeColor != undefined) {
        canvasContext.strokeStyle = strokeColor;
    }

    canvasContext.closePath();
    canvasContext.fillStyle = fillColor;
    canvasContext.fill();
}

export function drawImage(reference, sx, sy, sw, sh, dx, dy, dw, dh) {
    canvasContext.drawImage(reference, sx, sy, sw, sh, dx, dy, dw, dh);
}

export function drawQuadrilateral(fillColour, x1, y1, x2, y2, x3, y3, x4, y4) {
    canvasContext.beginPath();
    canvasContext.moveTo(x1, y1);
    canvasContext.lineTo(x2, y2);
    canvasContext.lineTo(x3, y3);
    canvasContext.lineTo(x4, y4);    
    canvasContext.fillStyle = fillColour;
    canvasContext.fill();
}

export function drawText(text, x, y, font, colour, isFilled) {    
    canvasContext.font = font;

    if (isFilled) {
        canvasContext.fillStyle = colour;
        canvasContext.fillText(text, x, y);
    }
    else {
        canvasContext.strokeStyle = colour;
        canvasContext.strokeText(text, x, y);
    }
}

export function drawTrapezium(fillColour, x1, y1, w1, x2, y2, w2) {    
    canvasContext.beginPath();
    canvasContext.moveTo(x1 - w1, y1);
    canvasContext.lineTo(x2 - w2, y2);
    canvasContext.lineTo(x2 + w2, y2);
    canvasContext.lineTo(x1 + w1, y1);    
    canvasContext.fillStyle = fillColour;
    canvasContext.fill();
}

function onResizeCanvas() {
    canvasElement.style.width = "100%";
    canvasElement.style.height = "100%";
    
    const width = canvasElement.clientWidth;
    const height = canvasElement.clientHeight;

    if (canvasElement.width !== width || canvasElement.height !== height) {
        canvasElement.width = width;
        canvasElement.height = height;        
    }

    componentReference.OnResizeCanvas(canvasElement.width, canvasElement.height);    
}