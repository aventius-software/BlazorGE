let blazorGECoreInstance = null;
let blazorGELastFrameTime = 0;
let blazorGECanvas = null;
let mouseCoords = null;
let blazorGECanvasOffset = 0;
let blazorGECanvasOffsetX = 0;
var blazorGECanvasOffsetY = 0;

import * as blazorGE2d from '/_content/BlazorGE.Graphics2D/interop2d.js';

function gameLoop(timeStamp) {
    // Calculate time difference since last frame
    var timeDifference = timeStamp - blazorGELastFrameTime;

    // Call Blazor game loop
    blazorGECoreInstance.invokeMethodAsync('GameLoop', timeStamp, timeDifference, 1.0 / (timeDifference / 1000));

    // Save the time of the rendered frame
    blazorGELastFrameTime = timeStamp;

    // Next...
    window.requestAnimationFrame(gameLoop);
}

function getKeyCode(e) {
    // TODO: implement properly
    // see https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/keyCode
    var code;

    //if (e.key !== undefined) {
    //    code = e.key;
    //} else if (e.keyIdentifier !== undefined) {
    //    code = e.keyIdentifier;
    //} else if (e.keyCode !== undefined) {
        code = e.keyCode;
    //}

    return code;
}

function handleMouseDown(e) {
    mouseX = parseInt(e.clientX - offsetX);
    mouseY = parseInt(e.clientY - offsetY);

    // Put your mousedown stuff here

    blazorGECoreInstance.invokeMethodAsync('OnKeyDown', getKeyCode(e));

}

function getMouseCoords(e) {
    let x = parseInt(e.clientX - blazorGECanvasOffsetX);
    let y = parseInt(e.clientY - blazorGECanvasOffsetY);
    return ({ x, y });
}

function getOffset(element) {
    if (!element.getClientRects().length) {
        return { top: 0, left: 0 };
    }

    let rect = element.getBoundingClientRect();
    let win = element.ownerDocument.defaultView;
    return (
        {
            top: rect.top + win.pageYOffset,
            left: rect.left + win.pageXOffset
        });
}

export function addMouseCanvasHandlers(instance) {

    blazorGECanvas = instance;

    blazorGECanvasOffset = getOffset(blazorGECanvas);
    blazorGECanvasOffsetX = blazorGECanvasOffset.left;
    blazorGECanvasOffsetY = blazorGECanvasOffset.top;

    blazorGECanvas.addEventListener('mousemove', (e) => {
        mouseCoords = getMouseCoords(e);
        blazorGECoreInstance?.invokeMethodAsync('OnMouseMove', mouseCoords.x, mouseCoords.y);
    });
    blazorGECanvas.addEventListener('mouseenter', (e) => {
        mouseCoords = getMouseCoords(e);
        blazorGECoreInstance?.invokeMethodAsync('OnMouseMove', mouseCoords.x, mouseCoords.y);
    });
    //instance.addEventListener('mousewheel', (e) => {
    //    mouseCoords = getMouseCoords(e);
    //});
    blazorGECanvas.addEventListener('mousedown', (e) => {
        mouseCoords = getMouseCoords(e);
        blazorGECoreInstance?.invokeMethodAsync('OnMouseDown', mouseCoords.x, mouseCoords.y);
    });
    blazorGECanvas.addEventListener('mouseup', (e) => {
        mouseCoords = getMouseCoords(e);
        blazorGECoreInstance?.invokeMethodAsync('OnMouseUp', mouseCoords.x, mouseCoords.y);
    });
}

export function initialiseGame(instance) {    
    blazorGECoreInstance = instance;

    window.addEventListener('keydown', (e) => {        
        blazorGECoreInstance.invokeMethodAsync('OnKeyDown', getKeyCode(e));
    });

    window.addEventListener('keyup', (e) => {
        blazorGECoreInstance.invokeMethodAsync('OnKeyUp', getKeyCode(e));
    });

    window.requestAnimationFrame(gameLoop);
}