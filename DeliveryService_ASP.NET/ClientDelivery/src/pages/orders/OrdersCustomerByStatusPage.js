import { useState, useEffect } from "react";
import useOrderService from "../../services/OrderService";
import { Button } from "@mui/material";
import { Table, TableBody, TableCell, tableCellClasses, TableContainer, TableHead, TableRow, Paper} from "@mui/material";
import { styled } from '@mui/material/styles';
import { useNavigate, useParams} from "react-router-dom";
import {FaRegNewspaper} from 'react-icons/fa';
import './ordersCustomerByStatusPage.scss';

const OrdersCustomerByStatusPage = () => {

    const [data, setData] = useState(null);

    const navigate = useNavigate();
    const {status} = useParams();

  	const {getOrdersByCustomerByStatus} = useOrderService();

    useEffect(() => {
        getOrdersByCustomerByStatus(status)
            .then(data => setData(data))
	  }, [status]);

    console.log(data);

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

     const navigateOrders = (status) => {
        navigate(`/orders/${status}`);
     }

     const singlePage = (id) => {
        navigate(`/order/${id}`)
     }

    const renderItems = (data) => {
        return (
            <>
                <div className="button input">
                    <Button variant="contained" size="medium" type="submit" 
                        onClick={() => navigateOrders('Create')}
                        disabled={data.orders[0].status === 'Create' ? true : false}
                        >Созданные заказы
                    </Button>

                    <Button variant="contained" size="medium" type="submit" 
                        onClick={() => navigateOrders('Progress')}
                        disabled={data.orders[0].status === 'Progress' ? true : false}
                        >Заказы в пути
                    </Button>

                    <Button variant="contained" size="medium" type="submit" 
                        onClick={() => navigateOrders('Complete')}
                        disabled={data.orders[0].status === 'Complete' ? true : false}
                        >Завершенные заказы
                    </Button>
                </div>   
                <p></p>
            <TableContainer component={Paper}>
                <Table aria-label="simple table">
                    <TableHead>
                        <StyledTableRow>
                            <StyledTableCell align="center">Информация</StyledTableCell>
                            <StyledTableCell align="center">Дата создания</StyledTableCell>
                            <StyledTableCell align="center">Дата доставки</StyledTableCell>
                            <StyledTableCell align="center">Статус</StyledTableCell>
                            <StyledTableCell align="center">Описание</StyledTableCell>
                            <StyledTableCell align="center">Курьер</StyledTableCell>
                        </StyledTableRow>
                    </TableHead>
                    <TableBody>
                        {data.orders.map((order) => (
                            <StyledTableRow>
                                <StyledTableCell align="center">
                                    <button type="button" onClick={() => singlePage(order.orderId)} >
                                        <div className="icon">{<FaRegNewspaper/>}</div>
                                    </button>
                                </StyledTableCell>
                                <StyledTableCell align="center">{order.created.slice(0,10) + ' '+ order.created.slice(11,16)}</StyledTableCell>
                                <StyledTableCell align="center">{order.status === 'Complete' ?  (order.end.slice(0,10) + ' '+ order.end.slice(11,16)) : '...'}</StyledTableCell>
                                <StyledTableCell align="center">{order.status === 'Create' ? 'Обрабатываем...' : (order.status === 'Progress' ? 'Курьер в пути!' : 'Заказ доставлен!') }</StyledTableCell>
                                <StyledTableCell align="center">{order.description}</StyledTableCell>
                                <StyledTableCell align="center">{order.courier.lastName ? (order.courier.firstName + ' ' + order.courier.lastName) : 'Подбираем курьера' }</StyledTableCell>
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
        <div className='orderlist1__container'>
            {data !== null ? items : null}
        </div>
        
    )
}

export default OrdersCustomerByStatusPage;