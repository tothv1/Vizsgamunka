import rightIdle from "../Assets/characters/slime.png"
import { Distance, GetDirAngle, GetDirection, LerpNum, Normalise, RadToDegrees, Translate, getRandomRange } from "./Math";
import { HPbar } from "../render/HPBar";
import { Bow } from "./Weapons/Bow";


const getImg = () => {

  var temp = new Image();
  temp.src = rightIdle;
  return temp;
}


class Slime {
  ID = 1;
  frame = 0;
  frameDelay = 10; //every x updates; the sprite turns over to the next frame
  frameLength = 8; // frames in the spritesheet
  state = "idle";
  mirror = false;

  xpValue = 3;

  dead = false;

  rotation = 0;

  x = 0;
  y = 0;

  team = 2;

  maxHealth = 100;
  health = 100;

  width = 64;
  height = 64;

  xhitbox = 32;
  yhitbox = 32;

  damagable = true;

  renderoffset = [0, 0];

  speed = 100 * getRandomRange(0.9, 1.1);
  health = 100;

  xcenter = 0;
  ycenter = 0;

  entityRef = [];

  aimPoint = [0, 0];
  windowSize = [0, 0];
  weapons = [];

  drawing = getImg();
  hpbar = Object.create(HPbar)


  init() {

    let temp = new Bow();
    temp.owner = this;
    temp.firerate = 1;
    temp.active = true;
    this.weapons.push(temp);

  }

  Update(deltaTime, frameCount, target) {

    this.aimPoint = [target.x, target.y]

    this.frameLength = this.drawing.width / this.width;

    if (this.frameLength > 1) {
      if (frameCount % this.frameDelay === 0) {
        this.frame++;
      }
      if (this.frame >= this.frameLength) {
        this.frame = 0
      }
    }

    let dir = GetDirection([this.x, this.y], [this.aimPoint[0], this.aimPoint[1]]);
    let normDir = Normalise(dir);


    let translation = Translate([this.x, this.y], [normDir[0] * this.speed * deltaTime, normDir[1] * this.speed * deltaTime]);

    this.x = translation[0];
    this.y = translation[1];

    this.xcenter = this.x - (this.width / 2);
    this.ycenter = this.y - (this.height / 2);





    this.weapons.forEach(weapon => {
      weapon.Update(deltaTime, frameCount)
    });
  };

  takeDamage(damage) {
    this.health -= damage;
    if (this.health <= 0) {
      this.dead = true;
    }
  }

}





export { Slime };