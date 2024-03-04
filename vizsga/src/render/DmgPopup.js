import { Clamp, Normalise,GetDirection, GetDirRad } from "../system/Math";

const DMGpopup = {
    ID : 0,
  
    rotation : 0,
  
    x : 0,
    y : 0,

    spd:100,

    damage : 0,
    size:0,

    frame : 0,
    maxFrame: 60,

    offset : [0,0],
  
    entityRef:[],
    update:Update
  
  }

  function Update(deltaTime, frameCount) {

  
    this.xcenter = this.x+this.width/2;
    this.ycenter = this.y+this.height/2;

    this.y-=this.spd*deltaTime;

    var drift = Math.random()*10;

    this.x-=drift*deltaTime

    this.frame++;
  }
  export { DMGpopup };