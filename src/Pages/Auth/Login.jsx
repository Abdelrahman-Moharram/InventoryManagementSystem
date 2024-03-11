import React, { useState } from 'react'
import { useDispatch } from 'react-redux'
import { AuthLogin } from '../../store/authSlice'
import { Link, useNavigate } from 'react-router-dom'
import logo from '../../assets/logo.png'
const Login = () => {
    const [username, setUsername] = useState("")
    const [password, setPassword] = useState("")
    const [showPassword, setShowPassword] = useState(false)
    const [error, setError] = useState("")
    const dispatch = useDispatch()
    const nav = useNavigate()

    const handleForm = (e) =>{
        e.preventDefault()
        if(username && password){
            dispatch(AuthLogin({username, password}))
            .unwrap()
            .then(()=>{
                setPassword("")
                setUsername("")
                nav("/")
            }).catch(err=>setError(err))
            
            
        }
    }
  return (


<div className="mx-auto max-w-screen-xl px-4 py-16 sm:px-6 lg:px-8">
  <div className="mx-auto max-w-lg">
    <h1 className="text-center text-2xl font-bold text-indigo-600 sm:text-3xl">Get started today</h1>

    <p className="mx-auto mt-4 max-w-md text-center text-gray-500">
      Manage your products with some touch
    </p>

    <form className="mb-0 mt-6 space-y-4 rounded-lg p-4 default-shadow sm:p-6 lg:p-8"  method='post' onSubmit={handleForm}>
      <p className="text-center text-lg font-medium">Sign in to your account</p>

      <div>
        <label htmlFor="email" className="sr-only">Email</label>

        <div className="relative">
          <input
            name="username"
            value={username}
            onChange={e=>setUsername(e.target.value)}
            className="w-full rounded-lg border-gray-200 p-4 pe-12 text-sm shadow-sm"
            placeholder="Enter email"
          />

          
        </div>
      </div>

      <div>
        <label htmlFor="password" className="sr-only">Password</label>

        <div className="relative">
          <input
            value={password}
            onChange={e=>setPassword(e.target.value)}
            className="w-full rounded-lg border-gray-200 p-4 pe-12 text-sm shadow-sm"
            placeholder="Enter password"
            type={showPassword?"text":'password'}
          />

          <span className="absolute inset-y-0 end-0 grid place-content-center px-4" onClick={()=>setShowPassword(!showPassword)}>
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="size-4 text-gray-400"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"
              />
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z"
              />
            </svg>
          </span>
        </div>
      </div>

      <button
        type="submit"
        className="block w-full rounded-lg bg-indigo-600 px-5 py-3 text-sm font-medium text-white"
      >
        Sign in
      </button>

      <p className="text-center text-sm text-gray-500">
        No account?
        <a className="underline" href="#">Sign up</a>
      </p>
    </form>
  </div>
</div>
  )
}

export default Login