import React from 'react'
import { Link } from 'react-router-dom'
import 'bootstrap/dist/css/bootstrap.min.css'

const AuthPage = () => {
    return (
        <div className='container-fluid d-flex align-items-center justify-content-md-center text-center vh-100'>
            <div>
                <Link to={'/login'} className='btn btn-primary'>Bejelentkezés</Link><br />
                <Link to='/register'>Még nincs fiókod? Regisztrálj a szövegre kattintva.</Link>
            </div>
        </div>
    )
}

export default AuthPage