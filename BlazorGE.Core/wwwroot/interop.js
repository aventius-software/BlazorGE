function gameLoop(timeStamp) {
    // Calculate time difference since last frame
    var timeDifference = timeStamp - window.blazorGELastFrameTime;
    
    // Skip the frame if the call is too early, code taken and modified from example
    // here https://riptutorial.com/html5-canvas/example/18718/set-frame-rate-using-requestanimationframe
    //if (timeDifference < blazorGEMinFrameTime) {
    //    window.requestAnimationFrame(gameLoop);
    //    return;
    //}

    // Call Blazor game loop
    window.blazorGECoreInstance.invokeMethodAsync('GameLoop', timeStamp, timeDifference, 1.0 / (timeDifference / 1000));

    // Save the time of the rendered frame
    window.blazorGELastFrameTime = timeStamp;

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

export function initialiseGame(instance, defaultTargetFPS) {    
    window.blazorGECoreInstance = instance;    

    window.addEventListener('keydown', (e) => {        
        window.blazorGECoreInstance.invokeMethodAsync('OnKeyDown', getKeyCode(e));
    });

    window.addEventListener('keyup', (e) => {
        window.blazorGECoreInstance.invokeMethodAsync('OnKeyUp', getKeyCode(e));
    });

    // Initialise the FPS
    //setTargetFramesPerSecond(defaultTargetFPS);
    window.requestAnimationFrame(gameLoop);
}

export function setTargetFramesPerSecond(targetFPS) {
    // Set to 60 if invalid value passed
    if (targetFPS < 1) targetFPS = 60

    // Set the target FPS we want (e.g. 60)
    window.blazorGETargetFPS = targetFPS;

    // Set the min time to render the next frame, code taken and modified from example
    // here https://riptutorial.com/html5-canvas/example/18718/set-frame-rate-using-requestanimationframe
    window.blazorGEMinFrameTime = (1000 / 60) * (60 / window.blazorGETargetFPS) - (1000 / 60) * 0.5;
    window.blazorGELastFrameTime = 0;
}