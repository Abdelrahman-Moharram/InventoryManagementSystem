import React from 'react'
import InfoSkeleton from '../Sekeltons/InfoSkeleton'
import { Link } from 'react-router-dom'


const ProductInfo = ({product}) => {
    return (
      <div className='w-3/4 py-5 px-12 rounded-lg default-shadow'>
          {
              product?.name?
                  <div>
                      <h2 className='text-[20px]'>{product?.name}</h2>
                      <Link  href={`/brands/${product?.brandId}`}  className="flex text-gray-600 hover:text-gray-500 text-[13px] mb-3">
                          {product?.brandName}
                      </Link>
                      {
                          product?.colors?
                          
                          <div>
                              <span className="text-xl">Available Colors</span>
                              <ul className='flex justify-start gap-2  mb-4'>
                                  {product?.colors?.map(cl=>
                                  <li key={cl} className='cursor-pointer rounded-full shadow-lg shadow-neutral-500 bg-light p-1'>
                                      <div className="sr-only">{cl}</div>
                                      <div className={`rounded-full w-[25px] border border-black-500 h-[25px]`} style={{backgroundColor:cl}}></div>
                                  </li>
                                  )}
                              </ul>
                          </div>
                      :""
                      }
                      {
                          product?.price?
                          <>
                              <span className="text-xl">Price</span>
                              <p className='text-[17px] mb-7'> {product?.price} EGP</p>
                          </>
                          :null
                      }
                      {
                          product?.amount != null?
                          <>
                              <span className="text-xl">Amount in Stock</span>
                              <p className='text-[17px] mb-7'> {product?.amount} </p>
                          </>
                          :null
                      }
                      
                      {
                          product?.description?
                          <>
                              <span className="text-xl">Description</span>
                              <p className='text-[17px] mb-7'> {product?.description}</p>
                          </>
                          :null
                      }
                      

                  </div>
              :
              <InfoSkeleton />
          }
        
  
  
      </div>
    )
  }
  
  export default ProductInfo