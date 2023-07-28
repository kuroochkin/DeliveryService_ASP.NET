import { useState } from 'react';
import { NavLink, Link, useNavigate } from 'react-router-dom';
import './sidebar.scss';

import {
    FaBars,
    FaUserAlt,
    FaSignOutAlt,
    FaHourglassHalf,
    FaRocket,
    FaFlag,}
    from 'react-icons/fa';


const SidebarForCourier = ({setIsAuth}) => {

    const[isOpen ,setIsOpen] = useState(true);

    const navigate = useNavigate();

    const toggle = () => setIsOpen (!isOpen);

    const handleAuth = () => {
        sessionStorage.clear();
        setIsAuth(false);
        navigate("/login");
    }

    const menuItem=[   
        {
            path:"/profile",
            name:"Мой профиль",
            icon:<FaUserAlt/>
        },
        {
            path:"/allOrdersByCreate",
            name:"Непринятые заказы",
            icon:<FaHourglassHalf/>
        },
        {
            path:"/courierOrders/Progress",
            name:"Действующие заказы",
            icon:<FaRocket/>
        },
        {
            path:"/courierOrders/Complete",
            name:"Завершенные заказы",
            icon:<FaFlag/>
        }
    ]

    return (
        <div className="container">
           <div style={{width: isOpen ? "200px" : "50px"}} className="sidebar">
               <div className="top_section">
                <Link to={"/courier"}>
                    <h1 style={{display: isOpen ? "block" : "none", "color": "white"}} className="logo">Delivery</h1>
                </Link>
                   <div style={{marginLeft: isOpen ? "25px" : "0px"}} className="bars">
                       <FaBars onClick={toggle}/>
                   </div>
               </div>
               {
                   menuItem.map((item, index)=>(
                       <NavLink to={item.path} key={index} className="link" activeclassName="active">
                           <div className="icon">{item.icon}</div>
                           <div style={{display: isOpen ? "block" : "none"}} className="link_text">{item.name}</div>
                       </NavLink>
                   ))
               }
                <NavLink to="/login" className="link" onClick={() => handleAuth()} activeclassName="active">
                    <div className="icon">{<FaSignOutAlt/>}</div>
                    <div style={{display: isOpen ? "block" : "none"}} className="link_text">Выход</div>
                </NavLink>
           </div>
        </div>
    );
}

export default SidebarForCourier;