using AutoMapper;
using Hackaton.Application.DataTransferObject;
using Hackaton.Application.Interfaces.Persistence;
using Hackaton.Application.Mappings;
using Hackaton.Application.Models.Employee;
using Hackaton.Domain.Exceptions.NotFoundException;
using Hackaton.Service;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Employee.UnitTests
{
    public class EmployeeServiceTest
    {
        private EmployeeService _employeeService;
        private readonly Mock<IRepositoryManager> _employeeRepoMock = new();
        private readonly IMapper _mapper = new MapperConfiguration(x => x.AddProfile<MappingProfile>()).CreateMapper();
        public EmployeeServiceTest()
        {
            _employeeService = new EmployeeService(_employeeRepoMock.Object, _mapper);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfEmployees_WithOneEmployee()
        {
            //Arrage
            var employeeParameters = new EmployeeParameters();
            _employeeRepoMock
                .Setup(rep => rep.Employee.GetAllAsync(employeeParameters, CancellationToken.None, false))
                .ReturnsAsync(EmployeeServiceData.GetAllEmployees_WithOneEmployee());
            //Act
            var result = await _employeeService.GetAllAsync(employeeParameters, CancellationToken.None);
            //Assert
            Assert.Single(result.Item1);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfEmployees_WithTwoEmployee()
        {
            //Arrage
            var employeeParameters = new EmployeeParameters();
            _employeeRepoMock
                .Setup(rep => rep.Employee.GetAllAsync(employeeParameters, CancellationToken.None, false))
                .ReturnsAsync(EmployeeServiceData.GetAllEmployees_WithTwoEmployee());
            //Act
            var result = await _employeeService.GetAllAsync(employeeParameters, CancellationToken.None);
            //Assert
            Assert.Equal(2, result.Item1.Count());
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnAnEmployee_WhenExist()
        {
            //Arrage
            Guid employeeId = Guid.NewGuid();
            Hackaton.Domain.Entities.Employee employee = new() { Id = employeeId, Name = "Sr.Teste" };
            var employeeParameters = new EmployeeParameters();
            _employeeRepoMock
                .Setup(rep => rep.Employee.GetByIdAsync(employeeId, CancellationToken.None, false))
                .ReturnsAsync(employee);
            //Act
            var result = await _employeeService.GetByIdAsync(employeeId);
            //Assert
            Assert.Equal(employeeId, result.Id);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldThrowEmployeeNotFoundException_WhenNotExist()
        {
            //Arrage
            _employeeRepoMock
                .Setup(rep => rep.Employee.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None, false))
                .ReturnsAsync(() => null);
            //Act
            Task result() => _employeeService.GetByIdAsync(Guid.NewGuid());
            //Assert
            await Assert.ThrowsAsync<EmployeeNotFoundException>(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateAnEmployee_WhenEmployeeForCreationDtoIsValid()
        {
            //Arrage
            Hackaton.Domain.Entities.Employee? employee = null;
            EmployeeForCreationDto employeeForCreationDto = new() { Name = "Sr.Teste", Email = "teste@mail.com" };

            _employeeRepoMock.Setup(rep => rep.Employee.InsertAsync(It.IsAny<Hackaton.Domain.Entities.Employee>(), CancellationToken.None))
                .Callback<Hackaton.Domain.Entities.Employee, CancellationToken>((call, call2) => employee = call);
            _employeeRepoMock.Setup(rep => rep.UnitOfWork.SaveChangesAsync(CancellationToken.None));
            //Act
            await _employeeService.CreateAsync(employeeForCreationDto, CancellationToken.None);
            //Assert
            _employeeRepoMock.Verify(rep => rep.Employee.InsertAsync(It.IsAny<Hackaton.Domain.Entities.Employee>(), CancellationToken.None), Times.Once);
            Assert.Equal(employee?.Name, employeeForCreationDto.Name);
            Assert.Equal(employee?.Email, employeeForCreationDto.Email);
        }
    }
}
