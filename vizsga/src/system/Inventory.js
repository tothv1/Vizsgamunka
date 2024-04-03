import React from 'react';

const Inventory = () => {
    return (
        <div style={{ textAlign: 'center', position: 'fixed', bottom: '10px', left: '50%', transform: 'translateX(-50%)' }}>
            <div style={{ display: 'inline-flex', gap: '10px' }}>
                <div style={{ width: '50px', height: '50px', border: '1px solid black' }}></div> 
                <div style={{ width: '50px', height: '50px', border: '1px solid black' }}></div> 
            </div>
        </div>
    );
}

export default Inventory;