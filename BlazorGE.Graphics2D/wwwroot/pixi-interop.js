/*
import * as PIXI from './pixi.js'

export function initialise() {
    let type = "WebGL";
    if (!PIXI.utils.isWebGLSupported()) {
        type = "canvas"
    }

    PIXI.utils.sayHello(type);
}
*/

import { Application, Sprite, SCALE_MODES } from './pixi-module.js';

const app = new Application();
document.body.appendChild(app.view);

// Load the bunny texture
app.loader.add('bunny', 'https://pixijs.io/examples/examples/assets/bunny.png')
    .load(startup);

function startup() {
    const { texture } = app.loader.resources.bunny;
    texture.baseTexture.scaleMode = SCALE_MODES.NEAREST;
    var bunny = new Sprite(texture);

    // Center the sprite's anchor point
    bunny.anchor.set(0.5);
    bunny.scale.set(5);

    // Move the sprite to the center of the screen
    bunny.x = app.renderer.width / 2;
    bunny.y = app.renderer.height / 2;

    app.stage.addChild(bunny);

    // Listen for animate update
    app.ticker.add(function (delta) {
        // Rotate mr rabbit clockwise
        bunny.rotation += 0.1 * delta;
    });
}
