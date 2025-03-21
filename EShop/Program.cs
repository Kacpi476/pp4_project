
using EShop.Repositories;
using EShop.Services;

namespace EShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IClientRepository, ClientRepository>();
            builder.Services.AddSingleton<IClientService, ClientService>();
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<IInvoiceRepository, InvoiceRepository>();
            builder.Services.AddSingleton<IInvoiceService, InvoiceService>();
            builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
            builder.Services.AddSingleton<IOrderService, OrderService>();
            builder.Services.AddSingleton<ICartRepository, CartRepository>();
            builder.Services.AddSingleton<ICartService, CartService>();
            builder.Services.AddSingleton<IPaymentRepository, PaymentRepository>();
            builder.Services.AddSingleton<IPaymentService, PaymentService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
