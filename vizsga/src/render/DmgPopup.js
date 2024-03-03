import { Clamp, Normalise,GetDirection, GetDirRad } from "../system/Math";

const DMGpopup = {
    ID : 0,
  
    rotation : 0,
  
    x : 0,
    y : 0,

    damage : 0,

    frame : 0,
    maxFrame: 0,

    offset : [0,0],
  
    entityRef:[],
    update:Update
  
  }

  function Update(deltaTime, frameCount) {

    this.x = Clamp(this.x, 0, this.mapsize[0]-this.width);
    this.y = Clamp(this.y, 0, this.mapsize[1]-this.height);
  
    this.xcenter = this.x+this.width/2;
    this.ycenter = this.y+this.height/2;

    this.frame++;
  }
  export { DMGpopup };