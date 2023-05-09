import React, { Component } from 'react'
import { useParams } from 'react-router-dom'
import FullGameViewComponent from './components/FullGameViewComponent'
import NavigationModule from './components/NavigationModule'
import SiteFooter from './components/SiteFooter'


export default function GameFullPage() {
    let {id} = useParams();
    return (
        <div>
            <NavigationModule></NavigationModule>
            <FullGameViewComponent Id={id}></FullGameViewComponent>
            <SiteFooter></SiteFooter>
        </div>
    )
}
