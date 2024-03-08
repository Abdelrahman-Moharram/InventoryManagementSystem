import React, { useEffect } from 'react'
import { useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'

const Authenticated = ({children}) => {
    let auth = useSelector((state)=>state.auth)
    const nav = useNavigate()

    useEffect(() => {
        if(!auth?.IsAuthenticated)
        nav("/auth/login")
      }, [auth.IsAuthenticated]);
    return(
        children 
    )
}


export default Authenticated