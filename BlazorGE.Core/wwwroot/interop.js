function gameLoop(timeStamp) {        
    window.blazorGECoreInstance.invokeMethodAsync('GameLoop', timeStamp);
    window.requestAnimationFrame(gameLoop);
}

export function initialiseGame(instance) {    
    window.blazorGECoreInstance = instance;

    window.addEventListener('keydown', (e) => {
        window.blazorGECoreInstance.invokeMethodAsync('OnKeyDown', e.keyCode);
    });

    window.addEventListener('keyup', (e) => {
        window.blazorGECoreInstance.invokeMethodAsync('OnKeyUp', e.keyCode);
    });
    
    window.requestAnimationFrame(gameLoop);
}