import leftEnemy1 from "../Assets/enemy/enemy1/ENEMY1-spritesheet-left.png";
import rightEnemy1 from "../Assets/enemy/enemy1/ENEMY1-spritesheet-right.png";

import leftEnemy2 from "../Assets/enemy/enemy1/SLIME-spritesheet-left.png";
import rightEnemy2 from "../Assets/enemy/enemy1/SLIME-spritesheet-right.png";

import { GetDirection, Normalise, Translate, getRandomRange, Distance } from "./Math";
import { HPbar } from "../render/HPBar";
import { DMGpopup } from "../render/DmgPopup";
import { Potion } from "./Pickups/Potion";



class Slime {
  ID = 1;
  TextureID = Math.floor(getRandomRange(0, 2));

  frameCount = 0;
  frame = 0;
  frameDelay = 10; //every x updates; the sprite turns over to the next frame
  frameLength = 8; // frames in the spritesheet
  state = 0;

  xpValue = 3;

  dead = false;

  rotation = 0;

  x = 0;
  y = 0;

  team = 2;

  width = 64;
  height = 64;

  xhitbox = 32;
  yhitbox = 32;

  damagable = true;

  Damage = Math.round(getRandomRange(10 * 0.8, 10 * 1.2));
  collisionDamageDelay = 0.5;
  nextCollisionDamage = 0;

  critChance = 0;
  critLevel = 0;


  renderoffset = [0, 0];

  speed = 100 * getRandomRange(0.9, 1.1);
  maxHealth = 100;
  health = 100;

  xcenter = 0;
  ycenter = 0;

  entityRef = [];

  aimPoint = [0, 0];
  windowSize = [0, 0];
  weapons = [];

  drawing = new Image();
  hpbar = Object.create(HPbar)


  init() {

    if (this.state === 0) {
      this.state = 1;

      if (this.TextureID == 0) {
        this.drawing.src = rightEnemy1;
      }
      if (this.TextureID == 1) {
        this.drawing.src = rightEnemy2;
      }
    }

    if (this.state === 1) {
      this.state = 0;

      if (this.TextureID == 0) {
        this.drawing.src = leftEnemy1;
      }
      if (this.TextureID == 1) {
        this.drawing.src = leftEnemy2;
      }

    }

  }

  Update(deltaTime, frameCount, target) {


    this.nextCollisionDamage -= deltaTime;

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


    if (target.x > this.x) {

      if (this.state === 0) {
        this.state = 1;

        if (this.TextureID == 0) {
          this.drawing.src = rightEnemy1;
        }
        if (this.TextureID == 1) {
          this.drawing.src = rightEnemy2;
        }
      }
    }
    if (target.x < this.x) {

      if (this.state === 1) {
        this.state = 0;

        if (this.TextureID == 0) {
          this.drawing.src = leftEnemy1;
        }
        if (this.TextureID == 1) {
          this.drawing.src = leftEnemy2;
        }

      }
    }

    this.weapons.forEach(weapon => {
      weapon.Update(deltaTime, frameCount)
    });

    if (Distance([this.x, this.y], [target.x, target.y]) <= 20 && this.nextCollisionDamage <= 0) {
      target.takeDamage(this);
      this.nextCollisionDamage = this.collisionDamageDelay;
    }

  };

  takeDamage(source) {

    let temp = new DMGpopup();
    temp.x = this.x;
    temp.y = this.y;
    temp.Damage = source.Damage;
    temp.size =Math.sqrt(Math.abs(source.Damage)) + 20;
    temp.drift = [getRandomRange(-100, 100), -500];
    temp.critLevel = source.critLevel;
    this.entityRef.effectList.push(temp);

    this.health -= source.Damage;
    if (this.health <= 0) {
      let roll = Math.random()*100;
      if(roll<20){
        let h = new Potion();

        h.x = this.x;
        h.y = this.y;

        this.entityRef.entityList.push(h);
        console.log(this.entityRef)
      }
      this.dead = true;
      source.source.statCard.kills++;
      console.log(source)
    }
  }

}

export { Slime };