import React from 'react'
import axios from 'axios';

const Login = () => {
    return (
        <div className='container w-50 border border-dark mt-5 auth-container'>
            <form onSubmit={async  (e) => { 
                e.preventDefault();
                e.persist();

                var res = await axios.post('https://localhost:7096/Auth/login', {
                    username: e.target.username.value,
                    password: e.target.password.value
                }).then(res => console.log(res.data)).catch(err => console.log(err));

            } } className='form-control'>
                <h1>Login</h1>
                <div className='form-group m-2'>
                    <label htmlFor='username'>Username</label>
                    <input type='text' className='form-control' id='username' />
                </div>
                <div className='form-group m-2'>
                    <label htmlFor='password'>Password</label>
                    <input type='password' className='form-control' id='password' />
                </div>
                <button type='submit' className='btn btn-primary'>BejelentkezÃ©s</button>
            </form>
        </div>
    )
}

export default Login