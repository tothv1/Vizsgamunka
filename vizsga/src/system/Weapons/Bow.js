import { Normalise,GetDirection,CreateProjectile, getRandomRange } from "../Math";
import { Arrow } from "../Projectile";


const Bow = {


    ID: 60,

    frameDelay: 10, //every x updates, the sprite turns over to the next frame
    frameLength: 1, // frames in the spritesheet

    x: 0,
    y: 0,
    rotation: 0,

    offset: [0,0],

    width: 64,
    height: 64,

    xcenter: 0,
    ycenter: 0,

    frame: 0,
    firerate: 0.1, //másodperc
    nextfire: 0,
    spread : 5,

    damage : 10,
    critChance : 20,
    critDamageMult:2,

    active: false,

    owner:[],
    update: Update,
    shoot:Shoot


}

function Shoot(aimPoint,source,aimOffset) {

    let direction = [0,0];

    if(source.ID===0){
        direction = GetDirection([source.x, source.y], [aimPoint[0] - source.renderoffset[0]-aimOffset[0], aimPoint[1] - source.renderoffset[1]-aimOffset[1]])
    }else{
        direction = GetDirection([source.x, source.y], [aimPoint[0], aimPoint[1] ])
    }




    let normalised = Normalise(direction);
  
    let xd = Object.create(Arrow);
    xd.damage = this.damage;
    
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

function Update(deltaTime, frameCount) {

    this.nextfire-=deltaTime;

    if (this.active && this.nextfire<=0)
    {
        this.nextfire=this.firerate;
        this.owner.entityRef.projectileList.push(this.shoot(this.owner.aimPoint,this.owner,this.owner.aimOffset))
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

export { Bow };