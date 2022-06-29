<script>
    import Sidebar from "./Sidebar.svelte";
    import Navbar from "./Navbar.svelte";

    export let products = [
        {
            name: "Coca",
            buyingPrice: "7000",
            sellingPrice: "10000",
            quantity: 100,
        },
        {
            name: "Pepsi",
            buyingPrice: "8000",
            sellingPrice: "11000",
            quantity: 48,
        },
        {
            name: "Sprite",
            buyingPrice: "6000",
            sellingPrice: "9000",
            quantity: 72,
        }
    ];
</script>

<main>
    <Navbar/>
    <div class="mainPage">
        <Sidebar/>
        <div class="mainContent">
            <div class="table-wrapper">
                <table>
                    <tr>
                        <th>No.</th>
                        <th>Name</th>
                        <th>BuyingPrice</th>
                        <th>SellingPrice</th>
                        <th>Quantity</th>
                    </tr>
                    {#each products as product, index}
                        <tr>
                            <td>{index + 1}</td>
                            <td>{product.name}</td>
                            <td>{product.buyingPrice}</td>
                            <td>{product.sellingPrice}</td>
                            <td>{product.quantity}</td>
                        </tr>
                    {/each}
                </table>
            </div>
        </div>
    </div>
</main>

<style>
    .mainPage {
        padding-top: 10px;
        margin-top: 40px;
        height: 1500px;
    }

    .mainContent {
        height: 100%;
        margin-left: max(150px, 10%);
        padding: 16px;
        background-color: whitesmoke;
    }

    .table-wrapper {
        /*
            Directly put "overflow-x: auto" in table will not make the table automatically overflow when needed
            The table only overflows when we put "overflow-x: auto" in a wrapper element which wraps the table
        */
        padding: 0;
        margin: 0;
        overflow-x: auto;
        border-radius: 8px;
        box-shadow: rgba(149, 157, 165, 0.2) 0 8px 24px;
    }

    table {
        border-collapse: collapse;
        width: 100%;
    }

    tr:nth-child(even) {
        color: #000;
        background-color: #b8e1fb;
    }
    
    th, td {
        padding: 0.8em;
    }

    th {
        color: #fff;
        background-color: #109bf1;
        width: 7em;
        text-align: left;
    }

    th:first-child {
        width: 3em;
    }

    th:nth-child(2) {
        width: 12em;
    }
</style>