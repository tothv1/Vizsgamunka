import React from 'react'
import { Link } from 'react-router-dom'

const AuthPage = () => {
    return (
        <div className='container authhatter'>
            <div>
                <button className='btn btn-primary'>Regisztráció</button><br />
                <Link to='/login'>Már van felhasználója? Bejelentkezés</Link>
                <br />
                <Link className='btn btn-primary' to='/game' >gameszkó teszteléshez gomb</Link>
            </div>
        </div>
    )
}

export default AuthPage