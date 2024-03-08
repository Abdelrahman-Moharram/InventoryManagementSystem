import React from 'react'
import Nav from '../../Components/Navs/Nav'
import { Outlet } from 'react-router-dom'
import Footer from '../../Components/Footer/Footer'
import BackToTop from '../../Components/Utils/BackToTop'

const Layout = () => {
  return (
      <>
      <Nav />
          <Outlet />
      <Footer/>
      <BackToTop />
      </>
  )
}

export default Layout
