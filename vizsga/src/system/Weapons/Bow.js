import { Normalise,GetDirection,CreateProjectile, getRandomRange, GetDirRad } from "../Math";
import { Arrow } from "../Arrow";


class Bow {


    ID= 60;

    frameDelay= 10; //every x updates; the sprite turns over to the next frame
    frameLength= 1; // frames in the spritesheet

    x= 0;
    y= 0;
    rotation= 0;

    offset= [0,0];

    width= 64;
    height= 64;

    xcenter= 0;
    ycenter= 0;

    frame= 0;
    firerate= 0.1; //másodperc
    nextfire= 0;
    spread = 0.1;

    damage = 10;
    critChance = 20;
    critDamageMult=2;

    active= false;

    owner=[];

    Update(deltaTime, frameCount) {

        this.nextfire-=deltaTime;
    
        if (this.active && this.nextfire<=0)
        {
            this.nextfire=this.firerate*getRandomRange(0.9,1.1);

            this.owner.entityRef.projectileList.push(this.Shoot(this.owner.aimPoint,this.owner,this.owner.windowSize))
        }
    
        if (this.frameLength > 1) {
            if (this.frameCount % this.frameDelay === 0) {
                this.frame++;
            }
            if (this.frame >= this.frameLength) {
                this.frame = 0
            }
        }
    
        this.xcenter = this.x + this.width / 2;
        this.ycenter = this.y + this.height / 2;
    
    };



    Shoot(aimPoint,source,aimOffset) {

        let direction = [0,0];

        let xd = Object.create(Arrow);
        xd.damage = this.damage;

        if(source.ID===0){
            direction = GetDirection([source.x, source.y], [aimPoint[0] - source.renderoffset[0]-aimOffset[0], aimPoint[1] - source.renderoffset[1]-aimOffset[1]])
        }else{
            direction = GetDirection([source.x, source.y], [aimPoint[0], aimPoint[1] ])
        }

        let normalised = Normalise(direction);
        let c = Math.cos(getRandomRange(-this.spread/2,this.spread));
        let s = Math.sin(getRandomRange(-this.spread/2,this.spread));

        let tempp = normalised;

        normalised = [tempp[0]*c + tempp[1]*-s,tempp[0]*s+tempp[1]*c];


        let critRoll = getRandomRange(0,100);
        
        let localCrit = this.critChance
        let critLevel = Math.floor(localCrit/100);
        localCrit-=critLevel*100;
    
        if(critRoll<=localCrit){
            critLevel++;
        }
        xd.critLevel = critLevel;
        xd.damage=xd.damage*Math.pow(this.critDamageMult,critLevel);
    
        let temp = CreateProjectile([source.x,source.y], normalised, xd, this.owner.team);
        return temp
    }


}

export { Bow };