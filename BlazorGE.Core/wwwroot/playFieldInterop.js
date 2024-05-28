var lastFrameTime = 0;
var componentReference = {};
var animationRequest = -1;
var isInitialised = false;

export async function initialiseModule(assemblyName) {
    if (!isInitialised) {
        isInitialised = true;

        // Get the assembly exports
        const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
        var exports = await getAssemblyExports(assemblyName + ".dll");

        // Save a reference to the Blazor component
        componentReference = exports.BlazorGE.Core.Components.PlayField;

        // Add keyboard event listeners
        window.addEventListener("keydown", (e) => {
            componentReference.OnKeyDown(getKeyCode(e));
        });

        window.addEventListener("keyup", (e) => {
            componentReference.OnKeyUp(getKeyCode(e));
        });

        // Kick off the game loop
        if (animationRequest != -1) window.cancelAnimationFrame(animationRequest);
        animationRequest = window.requestAnimationFrame(gameLoop);
    }
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

function gameLoop(timeStamp) {
    // Calculate time difference since last frame
    var timeDifference = timeStamp - lastFrameTime;

    // Call Blazor game loop .NET function  
    componentReference.GameLoop(timeStamp, timeDifference, 1.0 / (timeDifference / 1000));

    // Save the time of the rendered frame
    lastFrameTime = timeStamp;

    // Next...
    animationRequest = window.requestAnimationFrame(gameLoop);
}