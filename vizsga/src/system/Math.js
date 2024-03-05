import { Arrow } from "./Projectile";

export function Clamp(num, min, max) {
    if (num < min) return min;
    if (num > max) return max;
    if (min > max) return min;
    return num;
}

export function Normalise(value) {
    let Magnitude = Math.sqrt(Math.pow(value[0], 2) + Math.pow(value[1], 2))
    let normVector = [value[0] / Magnitude, value[1] / Magnitude]
    return normVector
}

export function GetDirection(point1, point2) {
    return [point1[0] - point2[0], point1[1] - point2[1]];
}

export function LerpNum(num, target, rate) {
    let diff = target - num;
    let lerp = diff * rate;
    return num + lerp;
}

export function Lerp2D(num, target, rate) {
    let diffx = num[0] - target[0];
    let diffy = num[1] - target[1];
    let lerpx = diffx * rate;
    let lerpy = diffy * rate;
    return [lerpx, lerpy];
}

export function Translate(num, translation) {
    return [num[0] - translation[0], num[1] - translation[1]]
}

export function RadToDegrees(radian) {
    return radian * (180 / Math.PI);
}

export function GetDirRad(direction) {
    return Math.atan2(direction[1], direction[0]) - Math.PI / 2;
}

export function CheckCollision(object1, object2) {

    if (object1.team == object2.team) {
        return false
    }

    let obj1bottom = object1.ycenter + (object1.yhitbox / 2);
    let obj1top = object1.ycenter - (object1.yhitbox / 2);
    let obj1right = object1.xcenter + (object1.xhitbox / 2);
    let obj1left = object1.xcenter - (object1.xhitbox / 2);

    let obj2bottom = object2.ycenter + (object2.yhitbox / 2);
    let obj2top = object2.ycenter - (object2.yhitbox / 2);
    let obj2right = object2.xcenter + (object2.xhitbox / 2);
    let obj2left = object2.xcenter - (object2.xhitbox / 2);

    let hit = true;

    if ((obj1bottom < obj2top) ||
        (obj1top > obj2bottom) ||
        (obj1right < obj2left) ||
        (obj1left > obj2right)) {
        hit = false;

    }

    return hit
}

export function getRandomRange(min, max) {
    var num = Math.random() * (max - min) + min;

    return num;
}

export function CreateProjectile(position, rotation, object) {

    let xd = Object.create(object);
    let temp = [obj:xd];


    console.log(position)

    
    console.log(temp)
    temp.setvals(position,rotation);

    console.log(temp)


    //temp.direction = Normalise(GetDirection([Player.x, Player.y], [e.pageX - Player.renderoffset[0], e.pageY - Player.renderoffset[1]]));
    //temp.rotation = GetDirRad(temp.direction);
    temp.rotation=rotation;
    temp.damage = Math.floor(getRandomRange(object.damage*0.8,object.damage*1.2));


    return temp;
}