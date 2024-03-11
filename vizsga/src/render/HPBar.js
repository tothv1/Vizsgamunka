const HPbar = {
    ID : 60,

    renderx:0,
    rendery:0,

    offset : [0,0],
    
    renderoffset : [0,0],
  
    width : 64,
    height : 10,

    ratio : 0,
    
    maxValue : 0,
    currentValue : 0,
  
    setval : setVal,
    owner : Object,
    
  }
  
  function setVal (maxHP,currentHP){
    this.maxValue = maxHP;
    this.currentValue=currentHP;
    this.ratio = currentHP/maxHP;
  }

  export {HPbar}