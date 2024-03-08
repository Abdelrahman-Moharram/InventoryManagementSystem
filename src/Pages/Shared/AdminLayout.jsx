import React from 'react'
import { Outlet } from 'react-router-dom'
import BackToTop from '../../Components/Utils/BackToTop'
import SideNav from '../../Components/Navs/SideNav'
import Nav from '../../Components/Navs/Nav'

const AdminLayout = () => {
  return (
    <>
      <Nav />
      <div className="flex ">
        <SideNav />
        <Outlet />
      </div>
      <BackToTop />
    </>
  )
}

export default AdminLayout
