import render from "../../Assets/XP.png"
import { Distance, Lerp2D, LerpNum } from "../Math";

const getImg = () => {

    var temp = new Image();
    temp.src = render;
    return temp;
}

class XP {
    ID=1000;

    value = 0;

    frame = 0;
    frameDelay = 10; //every x updates; the sprite turns over to the next frame
    frameLength = 8; // frames in the spritesheet
 
    x = 0;
    y = 0;

    width = 64;
    height = 64;
  
    damagable=false;

    xhitbox = 32;
    yhitbox = 32;

    offset = [];

    dead=false;

    drawing = getImg();

    Update(deltaTime, frameCount,target) {

        this.aimPoint = [target.x,target.y]
  
        this.frameLength = this.drawing.width / this.width;
      
        if (this.frameLength > 1) {
          if (frameCount % this.frameDelay === 0) {
            this.frame++;
          }
          if (this.frame >= this.frameLength) {
            this.frame = 0
          }
        }
        
        let translation = [LerpNum(this.x,target.x,deltaTime*6),LerpNum(this.y,target.y,deltaTime*6)];

        
      
        this.x=translation[0];
        this.y=translation[1];
      
        this.xcenter = this.x-(this.width/2);
        this.ycenter = this.y-(this.height/2);
      
        if (Distance([this.x,this.y],[target.x,target.y])<this.xhitbox){
            this.dead=true;
            target.XP+=this.value;
        }
    




    }
}

export { XP }