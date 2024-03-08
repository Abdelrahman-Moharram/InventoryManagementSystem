import React, { useEffect } from 'react'
import { useDispatch } from 'react-redux'
import { AuthLogout } from '../../store/authSlice'
import { useNavigate } from 'react-router-dom'

const Logout = () => {
    const dispatch = useDispatch()
    const nav = useNavigate()
    useEffect(()=>{
        dispatch(AuthLogout())
            .unwrap()
            .then(()=>{
                nav("/auth/login")
            }).catch(err=>{
                console.log(err);
                nav("/")
            })
    }, [dispatch])
  return (
    <div>
      
    </div>
  )
}

export default Logout
