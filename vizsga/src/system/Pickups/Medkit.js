import render from "../../Assets/items/HEALTH.png"
import { Distance, LerpNum, GetDirection, Normalise, Translate, getRandomRange } from "../Math";

const getImg = () => {

  var temp = new Image();
  temp.src = render;
  return temp;
}

class Potion {
  ID = 1001;

  value = 0;

  frame = 0;
  frameDelay = 10; //every x updates; the sprite turns over to the next frame
  frameLength = 8; // frames in the spritesheet

  x = 0;
  y = 0;

  width = 64;
  height = 64;

  damagable = false;
  Damage =Math.round(getRandomRange(-20*0.8,-20*1.2))

  xhitbox = 32;
  yhitbox = 32;

  offset = [];

  dead = false;

  drawing = getImg();

  Update(deltaTime, frameCount, target) {

    this.frameLength = this.drawing.width / this.width;

    if (this.frameLength > 1) {
      if (frameCount % this.frameDelay === 0) {
        this.frame++;
      }
      if (this.frame >= this.frameLength) {
        this.frame = 0
      }
    }

    if (Distance([this.x, this.y], [target.x, target.y]) < this.xhitbox) {
      
      this.dead = true;
      target.takeDamage(this)
    }
  }
}

export { Potion }