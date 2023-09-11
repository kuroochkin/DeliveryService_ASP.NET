import useOrderService from "../../../services/OrderService";
import { useState, useEffect } from "react";
import { Table, TableBody, TableCell, tableCellClasses, TableContainer, TableHead, TableRow, Paper} from "@mui/material";
import { styled } from '@mui/material/styles';
import {FaPlusCircle} from 'react-icons/fa';
import './allOrdersByCreate.scss';


const AllOrdersByCreate = () => {
    
    const [data, setData] = useState(null);
    const [Button, setButton] = useState(null);

    const {getAllOrdersByCreate, confirmOrder} = useOrderService();

    useEffect(() => {
        getAllOrdersByCreate()
            .then(data => setData(data))
	}, []);

    console.log(data);

    const AddOrderByCourier = async id => {
		
        const data = await confirmOrder({
            orderId: id
        });
	}

    const StyledTableCell = styled(TableCell)(({ theme }) => ({
        [`&.${tableCellClasses.head}`]: {
          background: "black",
          color: "white",
          fontSize: 20,
          fontFamily: 'Nunito',
          textAlign: 'center'
        },
        [`&.${tableCellClasses.body}`]: {
          fontSize: 18,
          fontFamily: 'Nunito',
          textAlign: 'center'
        },
      }));
      
      const StyledTableRow = styled(TableRow)(({ theme }) => ({
        '&:nth-of-type(odd)': {
          backgroundColor: theme.palette.action.hover,
        },
        // hide last border
        '&:last-child td, &:last-child th': {
          border: 0,
        },
      }));

    const renderItems = (data) => {
        return (
            <>
            <TableContainer component={Paper}>
                <Table aria-label="simple table">
                    <TableHead>
                        <StyledTableRow>
                            <StyledTableCell align="center">Дата создания</StyledTableCell>
                            <StyledTableCell align="center">Статус</StyledTableCell>
                            <StyledTableCell align="center">Описание</StyledTableCell>
                            <StyledTableCell align="center">Заказчик</StyledTableCell>
                            <StyledTableCell align="center">Принять заказ</StyledTableCell>
                        </StyledTableRow>
                    </TableHead>
                    <TableBody>
                        {data.orders.map((order) => (
                            <StyledTableRow >
                                <StyledTableCell align="center">
                                    {order.created.slice(0,10) + ' '+ order.created.slice(11,16)}
                                </StyledTableCell>
                                <StyledTableCell align="center">Ожидает курьера</StyledTableCell>
                                <StyledTableCell align="center">{order.description}</StyledTableCell>
                                <StyledTableCell align="center">
                                    {order.customer.firstName + ' ' + order.customer.lastName}
                                </StyledTableCell>
                                <StyledTableCell align="center">
                                    <button type="button" onClick={() => AddOrderByCourier(order.orderId)} >
                                        <div className="icon">{<FaPlusCircle/>}</div>
                                    </button>
                                </StyledTableCell>
                            </StyledTableRow>))}
                    </TableBody>
                </Table>
            </TableContainer>
            </>

        )
    }


    let items;

    if(data !== null){
        items = renderItems(data);
    }

    return (
        <div className='order_list__container'>
            {data !== null ? items : null}
        </div>
        
    )
}

export default AllOrdersByCreate;