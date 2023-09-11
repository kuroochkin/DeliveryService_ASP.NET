import { useNavigate, useParams } from "react-router-dom";
import { useState } from "react";
import useOrderService from "../../services/OrderService";
import { useEffect } from "react";
import { Button } from "@mui/material";
import './productsBySectionPage.scss';


const ProductsBySectionPage = ({cartItems, setCartItems}) => {

    const [dataSections, setDataSections] = useState(null);
    const [data, setData] = useState(null);

    const navigate = useNavigate();
    const {sectionId} = useParams();

    const {getProductsBySectionId, getAllSections} = useOrderService();

    useEffect(() => {
        getAllSections()
            .then(dataSections => setDataSections(dataSections));
    }, []);

    useEffect(() => {
        getProductsBySectionId(sectionId)
            .then(data => setData(data));
    }, [sectionId]);

    const navigateSection = (sectionId) => {
        navigate(`/product/${sectionId}`);
    };

    const addCart = (item) => {
        setCartItems(cartItems => [...cartItems, item]);
        return cartItems;
    };

    const renderSections = (data) => {
        const sections = data.sections.map(item => {
            return(
                <div className="button__input">
                    <Button variant="contained" size="medium" type="submit" 
                        onClick={() => navigateSection(item.sectionId)}>{item.name}
                    </Button>
                </div>   
            )
        })

        return sections;
    }

    const renderItems = (data) => {
        
        const items = data.products.map((item, i) => {

            return(
                <>
                <li className="products__item" key={i}>
                    
                    <img src={'../img/' + item.thumbnail} alt={item.title} className="products__item-img"/>
                    <div className="products__item-name">{item.title} </div>
                    <div className="products__item-price">{item.price + "₽"}</div>
                    <Button onClick={() => addCart(item)} disabled={cartItems.some(o => o.productId === item.productId)}>В корзину</Button>
                </li>
                </>
            )
        }) 

        return (
            <ul className="products__grid">
                {items}
            </ul>
        )
    }

    let items;
    if(data !== null){
        items = renderItems(data);
    }

    let sections;
    if(dataSections !== null){
        sections = renderSections(dataSections);
    }
    
    return (
        <div className="products__list">
            {sections}
            {items}
        </div>
    )
}

export default ProductsBySectionPage;