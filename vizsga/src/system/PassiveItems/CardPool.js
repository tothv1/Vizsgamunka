import { BaseCritCardSelectable } from "./CritItem";
import { BaseDMGCardSelectable } from "./BaseDMGStat";
import { BaseSpeedCardSelectable } from "./BaseSpeedStat";

class CardPool{
    card =[
    new BaseCritCardSelectable(),
    new BaseDMGCardSelectable(),
    new BaseSpeedCardSelectable()];
}


export { CardPool };