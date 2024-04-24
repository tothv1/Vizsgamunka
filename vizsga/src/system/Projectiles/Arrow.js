import render from "../../Assets/nyil.png"

import "../Math";
import { Translate } from "../Math";

const getImg = () => {

    var temp = new Image();
    temp.src = render;
    return temp;
};

class Arrow {
    ID = 100;

    frameCount = 0;
    frameDelay = 20; //every x updates; the sprite turns over to the next frame
    frameLength = 10; // frames in the spritesheet

    x = 1;
    y = 0;
    rotation = 0;

    xcenter = 0;
    ycenter = 0;

    renderx = 0;
    rendery = 0;

    source = Object;

    offset = [0, 0];

    xhitbox = 32;
    yhitbox = 32;

    width = 64;
    height = 64;

    projectileSpeed = 100;

    frame = 0;

    Damage = 0;
    Hits=[];

    dead = false;

    critLevel = 0;
    hitlimit = 2;

    direction = [0, 0];
    drawing = getImg();

    Update(deltaTime, frameCount) {

        this.frameLength = this.drawing.width / this.width;

        if (this.frameLength > 1) {
            if (frameCount % this.frameDelay === 0) {
                this.frame++;
            }
            if (this.frame >= this.frameLength) {
                this.frame = 0
            }
        }

        let translation = Translate([this.x, this.y], [this.direction[0] * this.projectileSpeed * deltaTime, this.direction[1] * this.projectileSpeed * deltaTime]);

        this.x = translation[0];
        this.y = translation[1];

        this.xcenter = this.x - (this.width / 2);
        this.ycenter = this.y - (this.height / 2);

    };
}

export { Arrow }