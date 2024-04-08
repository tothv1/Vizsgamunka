import { Arrow } from "./Projectiles/Arrow";

export function Clamp(num, min, max) {
    if (num < min) return min;
    if (num > max) return max;
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

export function Distance(point1, point2) {
    let dist = Math.hypot(point1[0] - point2[0], point1[1] - point2[1]);
    return dist;
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

export function RadianToDegrees(radian) {
    return radian * (180 / Math.PI);
}

export function GetDirectionRadian(direction) {
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

export function CheckInside(point, obj) {

    let hit = true;

    if (point[0] < obj.xOffset ||
        point[0] > obj.xOffset + obj.width ||
        point[1] < obj.yOffset ||
        point[1] > obj.yOffset + obj.height) {
        hit = false;
    }

    return hit
}

export function getRandomRange(min, max) {
    var num = Math.random() * (max - min) + min;

    return num;
}

export function CreateProjectile(position, direction, object, source) {

    const temp = object;

    temp.x = position[0];
    temp.y = position[1];
    temp.direction = direction;
    temp.rotation = GetDirectionRadian(direction);
    temp.source = source;
    temp.team = source.team;

    temp.Damage = Math.round(getRandomRange(object.Damage * 0.8, object.Damage * 1.2));

    return temp;
}

export function CreateRandomDirection() {
    let the = 2 * Math.PI * Math.random()

    let c = Math.cos(the);
    let s = Math.sin(the);

    return [c, s];

}

export function randomTest(){
    return Math.random()<0.5 ? true : false;
}