import React from 'react'
import { Link } from 'react-router-dom'
import 'bootstrap/dist/css/bootstrap.min.css'

const AuthPage = () => {
    return (
        <div className='container'>
            <div>
                <Link to={'/login'} className='btn btn-primary'>Bejelentkezés</Link><br />
                <Link to='/register'>Még nincs fiókod? Regisztrálj a szövegre kattintva.</Link>
                <br />
                <Link className='btn btn-primary' to='/game'>gameszkó teszteléshez gomb</Link>
            </div>
        </div>
    )
}

export default AuthPage