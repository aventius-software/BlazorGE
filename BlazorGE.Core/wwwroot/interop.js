let blazorGECoreInstance = null;
let blazorGELastFrameTime = 0;

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

function handleMouseUp(e) {
    mouseX = parseInt(e.clientX - offsetX);
    mouseY = parseInt(e.clientY - offsetY);

    // Put your mouseup stuff here
}

function handleMouseOut(e) {
    mouseX = parseInt(e.clientX - offsetX);
    mouseY = parseInt(e.clientY - offsetY);

    // Put your mouseOut stuff here
}

function handleMouseMove(e) {
    mouseX = parseInt(e.clientX - offsetX);
    mouseY = parseInt(e.clientY - offsetY);

    // Put your mousemove stuff here

}

export function addMouseCanvasHandlers(instance) {
    instance.addEventListener('mousemove', (e) => {
        handleMouseMove(e);
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