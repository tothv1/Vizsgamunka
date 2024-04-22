import { Normalise, GetDirection, CreateProjectile, getRandomRange } from "../Math";
import { Arrow } from "../Projectiles/Arrow";
import { bowBaseStatcard } from "./BowCard";
import Render from "../../Assets/weapon/BOW1.png";

class Bow {

    ID = 60;

    frameDelay = 10; //every x updates; the sprite turns over to the next frame
    frameLength = 1; // frames in the spritesheet

    x = 0;
    y = 0;
    rotation = 0;

    offset = [0, 0];

    width = 64;
    height = 64;

    xcenter = 0;
    ycenter = 0;

    frame = 0;

    statCard = new bowBaseStatcard();
    
    nextfire = 0;

    DamageMult = 1;
    Damage = 0;

    Level = 1;
    MaxLevel = 7;

    active = false;

    owner = Object;2

    RecalculateStats() {
        this.Damage = this.owner.StatCard.BaseDamage * this.statCard.DamageMult * this.owner.StatCard.DamageMult;
        this.statCard.Firerate = this.statCard.BaseFirerate / (1 / this.owner.StatCard.FirerateMult);
    }

    Update(deltaTime, frameCount) {

        this.nextfire -= deltaTime;

        if (this.active && this.nextfire <= 0) {
            this.nextfire = this.statCard.Firerate * getRandomRange(0.9, 1.1);

            this.owner.entityRef.projectileList.push(this.Shoot(this.owner.aimPoint, this.owner, this.owner.windowSize))
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

    Shoot(aimPoint, source, aimOffset) {

        let direction = [0, 0];

        let xd = new Arrow();
        xd.Damage = this.statCard.Damage;

        if (source.ID === 0) {
            direction = GetDirection([source.x, source.y], [aimPoint[0] - source.renderoffset[0] - aimOffset[0], aimPoint[1] - source.renderoffset[1] - aimOffset[1]])
        } else {
            direction = GetDirection([source.x, source.y], [aimPoint[0], aimPoint[1]])
        }

        let normalised = Normalise(direction);
        let c = Math.cos(getRandomRange(-this.statCard.spread / 2, this.statCard.spread/2));
        let s = Math.sin(getRandomRange(-this.statCard.spread / 2, this.statCard.spread/2));

        let tempp = normalised;

        normalised = [tempp[0] * c + tempp[1] * -s, tempp[0] * s + tempp[1] * c];

        let critRoll = getRandomRange(0, 100);

        let localCrit = this.owner.StatCard.critChance;
        let critLevel = Math.floor(localCrit / 100);
        localCrit -= critLevel * 100;

        if (critRoll <= localCrit) {
            critLevel++;
        }

        xd.critLevel = critLevel;
        xd.Damage = xd.Damage * Math.pow(this.owner.StatCard.critDamageMult, critLevel);
        xd.projectileSpeed = getRandomRange(this.statCard.projectileSpeed * 0.9, this.statCard.projectileSpeed * 1.1);

        let temp = CreateProjectile([source.x, source.y], normalised, xd, this.owner);

        return temp
    }

}

export { Bow };