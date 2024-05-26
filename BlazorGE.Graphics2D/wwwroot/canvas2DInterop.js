var canvasContext = {};
var canvasElement = {};
var componentReference = {};

export async function initialiseModule(assemblyName, canvasID) {    
    // Get the assembly exports
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    let exports = await getAssemblyExports(assemblyName + ".dll");    

    // Save a reference to the Blazor component
    componentReference = exports.BlazorGE.Graphics2D.Components.Canvas2D;

    // References to the canvas
    canvasElement = document.getElementById(canvasID);
    canvasContext = canvasElement.getContext("2d");

    // Catch canvas resize events
    window.addEventListener("resize", onResizeCanvas);
    onResizeCanvas();

    // Canvas mouse handling    
    canvasElement.addEventListener("mousemove", (e) => {
        let mouseCoords = getCanvasMouseCoords(e);
        componentReference.OnMouseMove(mouseCoords.x, mouseCoords.y);
    });

    canvasElement.addEventListener("mouseenter", (e) => {
        let mouseCoords = getCanvasMouseCoords(e);
        componentReference.OnMouseMove(mouseCoords.x, mouseCoords.y);
    });

    //canvasElement.addEventListener('mousewheel', (e) => {
    //    let mouseCoords = getMouseCoords(e);
    //});

    canvasElement.addEventListener("mousedown", (e) => {
        let mouseCoords = getCanvasMouseCoords(e);
        componentReference.OnMouseDown(mouseCoords.x, mouseCoords.y);
    });

    canvasElement.addEventListener("mouseup", (e) => {
        let mouseCoords = getCanvasMouseCoords(e);
        componentReference.OnMouseUp(mouseCoords.x, mouseCoords.y);
    });
}

export function clearRect(x, y, width, height) {
    canvasContext.clearRect(x, y, width, height);
}

export function drawFilledRectangle(colour, x, y, width, height) {
    canvasContext.fillStyle = colour;
    canvasContext.fillRect(x, y, width, height);
}

export function drawFilledPolygon(fillColor, strokeColor, coordinates) {    
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

/* Internal functions */

function getCanvasMouseCoords(e) {
    let canvasOffset = getOffset(canvasElement);    
    let x = parseInt(e.clientX - canvasOffset.left);
    let y = parseInt(e.clientY - canvasOffset.top);

    return ({ x, y });
}

function getOffset(element) {
    if (!element.getClientRects().length) {
        return { top: 0, left: 0 };
    }

    let rect = element.getBoundingClientRect();
    let win = element.ownerDocument.defaultView;

    return ({
        top: rect.top + win.pageYOffset,
        left: rect.left + win.pageXOffset
    });
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

    // Call .NET to set the new canvas sizes
    componentReference.OnResizeCanvas(canvasElement.width, canvasElement.height);    
}