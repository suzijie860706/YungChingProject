using Microsoft.EntityFrameworkCore;
using YungChingProject.Data;
using YungChingProject.Repositories;


namespace YungChingProject.Tests.Repositories
{
    public abstract class CRUDRepositoryTestsBase<TEntity> where TEntity : class, new()
    {
        private NorthwindContext _context;
        private CRUDRepository<TEntity, DbContext> _repository;
        protected DbSet<TEntity> _dbset;
        protected abstract void SeedData();  // For seeding data specific to each entity

        [SetUp]
        public void SetUp()
        {
            //使用 InMemory 資料庫
            var options = new DbContextOptionsBuilder<NorthwindContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new NorthwindContext(options);

            _dbset = _context.Set<TEntity>();
            //Seed資料
            SeedData();  
            _context.SaveChanges();

            _repository = new CRUDRepository<TEntity, DbContext>(_context);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


        [Test]
        public async Task CreateAsync_WhenCalled_AddsEntityToDbSetAndSaveChanges()
        {
            //Arrange
            var entity = await _repository.FindByIdAsync("Jacky");
            if (entity == null) throw new NullReferenceException();
            _context.Database.EnsureDeleted();
            _context.ChangeTracker.Clear();

            //Act
            await _repository.CreateAsync(entity);

            //Assert
            Assert.That(_dbset.Contains(entity), Is.True);
        }

        [Test]
        public async Task UpdateAsync_WhenCalled_UpdateToDbSetAndSaveChanges()
        {
            //Arrange
            var entity = await _repository.FindByIdAsync("Jacky");
            if (entity == null) throw new NullReferenceException();

            //Act
            await _repository.UpdateAsync(entity);

            //Assert
            Assert.That(_dbset.Contains(entity), Is.True);
        }

        [Test]
        public async Task DeleteAsync_WhenCalled_DeleteEntityFromDbSet()
        {
            //Arrange
            var entity = await _repository.FindByIdAsync("Jacky");
            if (entity == null) throw new NullReferenceException();

            //Act
            int count = await _repository.DeleteAsync(entity);

            //Assert
            Assert.That(_dbset.Contains(entity), Is.False);
        }

        [Test]
        public async Task FindByIdAsync_WhenCalled_RetrunsEntity()
        {
            //Arrange

            //Act
            var entity = await _repository.FindByIdAsync("Jacky");

            //Assert
            Assert.That(entity, Is.Not.Null);
        }

        [Test]
        public async Task FindByIdAsync_WhenIdNotFound_RetrunsNull()
        {
            //Arrange

            //Act
            var entity = await _repository.FindByIdAsync("NoOne");

            //Assert
            Assert.That(entity, Is.Null);
        }

        [Test]
        public async Task FindAsync_WhenCalled_RetrunsData()
        {
            //Arrange

            //Act
            var data = await _repository.FindAsync(x => true);

            //Assert
            Assert.That(data, Is.Not.Null);
            Assert.That(data.Count, Is.GreaterThan(0));
        }
    }
}
