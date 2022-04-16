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
</main>

<style>
    .mainPage {
        padding-top: 10px;
        margin-top: 40px;
        height: 1500px;
    }

    .mainContent {
        margin-left: max(150px, 10%);
        margin-top: 7px;
        padding: 4px 14px 4px 14px;
    }

    table {
        padding: 0;
        margin: 0;
        border: 2px solid black;
        border-collapse: collapse;
        width: 100%;
    }

    tr:nth-child(even) {
        background: powderblue;
    }

    th {
        border: 2px solid black;
        padding: 0.3em;
        margin: 2em;
        color: deepskyblue;
        width: 7em;
        text-align: left;
    }

    th:first-child {
        width: 3em;
    }

    th:nth-child(2) {
        width: 12em;
    }

    td {
        padding: 0.4em 0.3em;
    }
</style>