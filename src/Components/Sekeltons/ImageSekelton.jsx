import React from 'react'

const ImageSekelton = ({width, height}) => {
  return (
    <div 
        className={`
            bg-slate-400 rounded-lg animate-pulse`
        }
        style={{
          height:height,
          width:width
        }}
        >

    </div>
  )
}

export default ImageSekelton