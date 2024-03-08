import React from 'react'
import { FaArrowUp } from "react-icons/fa";

const BackToTop = () => {
  return (
    <div
    onClick={()=>window.scrollTo({top: 0, behavior: 'smooth'})} 
    className='fixed rounded-full bg-[#1a1a1a] bottom-10 right-10 cursor-pointer p-[15px]'
    >
        <FaArrowUp color='#fff' />
    </div>
  )
}

export default BackToTop
