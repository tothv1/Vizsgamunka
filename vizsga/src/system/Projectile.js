import render from "../Assets/nyil.png"

import "../system/Math";
import { GetDirection, Normalise,Translate } from "../system/Math";

function Update(deltaTime, frameCount, target) {

    this.frameLength = this.drawing.width / this.width;

    if (this.frameLength > 1) {
        if (frameCount % this.frameDelay === 0) {
            this.frame++;
        }
        if (this.frame >= this.frameLength) {
            this.frame = 0
        }
    }

    let translation = Translate([this.x, this.y], [this.direction[0] * this.speed * deltaTime, this.direction[1] * this.speed * deltaTime]);

    this.x = translation[0];
    this.y = translation[1];


};


const getImg = () => {

    var temp = new Image();
    temp.src = render;
    return temp;
}



const Arrow = {
    ID: 100,

    frameDelay: 20, //every x updates, the sprite turns over to the next frame
    frameLength: 10, // frames in the spritesheet

    x: 0,
    y: 0,
    rotation : 0,

    renderx: 0,
    rendery: 0,

    offset: [0, 0],

    width: 64,
    height: 64,

    speed: 1000,

    frame: 0,

    direction: [],
    drawing: getImg(),
    entityRef: [],
    update: Update

}

export {Arrow}