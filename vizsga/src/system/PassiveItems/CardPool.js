import { BaseCritCardSelectable } from "./CritItem";
import { BaseDMGCardSelectable } from "./BaseDMGStat";
import { BaseSpeedCardSelectable } from "./BaseSpeedStat";
import { BowCardSelectable } from "../Weapons/BowCard";
import { IceRodCardSelectable } from "../Weapons/IcerodCard";

class CardPool{
    card =[
    new BowCardSelectable(),
    new BaseCritCardSelectable(),
    new BaseDMGCardSelectable(),
    new BaseSpeedCardSelectable(),
   ];
}


export { CardPool };