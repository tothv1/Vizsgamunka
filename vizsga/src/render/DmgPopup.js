import { Clamp, Normalise,GetDirection, GetDirRad, getRandomRange } from "../system/Math";

const DMGpopup = {
    ID : 0,
  
    rotation : 0,
  
    x : 0,
    y : 0,

    spd:100,

    damage : 0,
    size:0,
    drift: [0,0],
    driftspd : [0,20],

    frame : 0,
    maxFrame: 60,

    offset : [0,0],
  
    entityRef:[],
    update:Update
  
  }

  function Update(deltaTime, frameCount) {

  
    this.xcenter = this.x+this.width/2;
    this.ycenter = this.y+this.height/2;

    this.drift = [this.drift[0]+this.driftspd[0],
                  this.drift[1]+this.driftspd[1]];

    this.x+=this.drift[0]*deltaTime;
    this.y+=this.drift[1]*deltaTime;
    

    this.frame++;
  }
  export { DMGpopup };