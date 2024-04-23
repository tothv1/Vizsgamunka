import { BaseCritCardSelectable } from "./CritItem";
import { BaseDMGCardSelectable } from "./BaseDMGStat";
import { BaseSpeedCardSelectable } from "./BaseSpeedStat";
import { BowCardSelectable } from "../Weapons/BowCard";

class CardPool{

    card =[
    new BowCardSelectable(),
    new BaseCritCardSelectable(),
    new BaseDMGCardSelectable(),
    new BaseSpeedCardSelectable()];
}


export { CardPool };