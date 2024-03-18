import right from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-right.png"
import left from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-left.png"
import up from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-up.png"
import down from "../Assets/characters/1.karakter/KNIGHT-SPRITESHEET-down.png"

import "../system/Math";
import { Clamp, getRandomRange } from "../system/Math";
import { HPbar } from "../render/HPBar";
import { StatCard } from "./StatCard";
import { Bow } from "./Weapons/Bow";
import { DMGpopup } from "../render/DmgPopup";
import { putStats } from "./Hooks/PutStats";

class Player {
  ID = 0;

  level = 1;
  currrentXP = 0;
  requiredXP = 10;
  XPRatio = 0;

  frameCount = 0;
  frameDelay = 20; //every x updates; the sprite turns over to the next frame
  frameLength = 10; // frames in the spritesheet
  state = "moving";
  direction = "right";

  width = 64;
  height = 64;

  rotation = 0;

  x = 1;
  y = 0;
  xcenter = 0;
  ycenter = 0;
  xhitbox = 32;
  yhitbox = 32;

  mapsize = [0, 0];
  offset = [0, 0];
  renderoffset = [0, 0];

  statCard = new StatCard();

  speed = 300;

  maxHealth = 100;
  health = 100;

  team = 1;
  shooting = false;
  aimPoint = [0, 0];
  windowSize = [0, 0];
  weapons = [];

  damagable = true;

  UpState = false;
  DownState = false;
  RightState = false;
  LeftState = false;

  frame = 0;

  Damage = 10;
  critChance = 20;
  critDamageMult=2;

  canvasRef = 0;

  drawing = new Image();
  entityRef = [];
  canvasRed = [];

  hpbar = Object.create(HPbar)

  Update(deltaTime, frameCount) {

    if (this.currrentXP >= this.requiredXP) {


      this.currrentXP -= this.requiredXP;
      this.level += 1;
      this.Damage+=1;
      this.critChance+=3;
      this.critDamageMult+=0.05;

      //let obj = new Bow();
      //obj.owner = this;
      //this.weapons.push(obj)

      //let linearNext = this.level*5; 
      //let exponentialNext = Math.floor(this.requiredXP*1.1);

      //console.log(linearNext);
      //console.log(exponentialNext);

      //linearNext > exponentialNext ? this.requiredXP=linearNext : this.requiredXP = exponentialNext;

      this.requiredXP = Math.ceil(this.requiredXP * 1.1);    //exponential scale
      //this.requiredXP=this.level*10;                     //linear scale
    }

    this.XPRatio = this.currrentXP / this.requiredXP;


    if (this.state === "moving") {
      if (this.direction === "left") {
        this.drawing.src = left;
      }
      if (this.direction === "right") {
        this.drawing.src = right;
      }
      if (this.direction === "up") {
        this.drawing.src = up;
      }
      if (this.direction === "down") {
        this.drawing.src = down;
      }

      if (frameCount % this.frameDelay === 0) {
        this.frame++;
      }
      if (this.frame >= this.frameLength) {
        this.frame = 0
      }
    } else {
      this.frame = 0;
      //drawing.src=idle;
      //width=64;
    }

    this.frameLength = this.drawing.width / this.width;

    if (!this.UpState && !this.DownState && !this.LeftState && !this.RightState) {
      this.state = "idle";
    } else {
      this.state = "moving";
    }

    //irány state, lezárja az irányt

    if (this.UpState) {
      this.y -= this.speed * deltaTime;
      if (this.direction === "none") {
        this.direction = "up";
      }
    }

    if (this.DownState) {
      this.y += this.speed * deltaTime;
      if (this.direction === "none") {
        this.direction = "down";
      }
    }

    if (this.RightState) {
      this.x += this.speed * deltaTime;
      if (this.direction === "none") {
        this.direction = "right";
      }
    }

    if (this.LeftState) {
      this.x -= this.speed * deltaTime;
      if (this.direction === "none") {
        this.direction = "left";
      }
    }

    this.x = Clamp(this.x, 0, this.mapsize[0] + this.width);
    this.y = Clamp(this.y, 0, this.mapsize[1] + this.height);

    this.xcenter = this.x - this.width / 2;
    this.ycenter = this.y - this.height / 2;

    this.weapons.forEach(weapon => {
      weapon.Update(deltaTime, frameCount)
    });


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

      putStats(this.statCard);
      this.dead = true;
    }
  }
  keyhandler(e) {

    if (e.type === "mouseup") {
      this.weapons.forEach(weapon => {
        weapon.active = false;
      });
    }
    if (e.type === "mousedown") {
      this.weapons.forEach(weapon => {
        weapon.active = true;
      });
    }

    if (e.type === "keydown") {
      if (e.key === "w") {

        this.UpState = true;
      }
      if (e.key === "s") {

        this.DownState = true;
      }
      if (e.key === "a") {

        this.LeftState = true;
      }
      if (e.key === "d") {

        this.RightState = true;
      }

    }

    // felengedésen kinyitja az irány lock-ot, rendereléshez kell
    if (e.type === "keyup") {
      if (e.key === "w") {
        this.UpState = false;
        if (this.direction === "up") this.direction = "none";
      }
      if (e.key === "s") {
        this.DownState = false;
        if (this.direction === "down") this.direction = "none";
      }
      if (e.key === "a") {
        this.LeftState = false;
        if (this.direction === "left") this.direction = "none";
      }
      if (e.key === "d") {
        this.RightState = false;
        if (this.direction === "right") this.direction = "none";
      }
    }
  }
}

export { Player };