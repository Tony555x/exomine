using exomine.Data.Enums;
using exomine.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AchievementConfiguration:IEntityTypeConfiguration<Achievement>{
    public void Configure(EntityTypeBuilder<Achievement> builder)
    {
        builder.HasData(AchievementList());
    }
    List<Achievement> AchievementList()
    {

        return [
            new Achievement { Id = 1, Name = "Win any game at least size 5", GridType = null, MinSize = 5 },
            new Achievement { Id = 2, Name = "Win any game at least size 10", GridType = null, MinSize = 10 },

            new Achievement { Id = 3, Name = "Win a hexagon game at size 5", GridType = GridType.Hexagon, MinSize = 5 },
            new Achievement { Id = 4, Name = "Win a hexagon game at size 10", GridType = GridType.Hexagon, MinSize = 10 },
            new Achievement { Id = 5, Name = "Win a hexagon game at difficulty at least 20", GridType = GridType.Hexagon, MinDifficulty = 20 },
            new Achievement { Id = 6, Name = "Win a hexagon game at difficulty at least 20 under 30 seconds", GridType = GridType.Hexagon, MinDifficulty = 20, MaxTimeSeconds = 120 },

            new Achievement { Id = 7, Name = "Win a square game at size 5", GridType = GridType.Square, MinSize = 5 },
            new Achievement { Id = 8, Name = "Win a square game at size 10", GridType = GridType.Square, MinSize = 10 },
            new Achievement { Id = 9, Name = "Win a square game at difficulty at least 20", GridType = GridType.Square, MinDifficulty = 20 },
            new Achievement { Id = 10, Name = "Win a square game at difficulty at least 20 under 30 seconds", GridType = GridType.Square, MinDifficulty = 20, MaxTimeSeconds = 30 },

            new Achievement { Id = 11, Name = "Win a triangle game at size 5", GridType = GridType.Triangle, MinSize = 5 },
            new Achievement { Id = 12, Name = "Win a triangle game at size 10", GridType = GridType.Triangle, MinSize = 10 },
            new Achievement { Id = 13, Name = "Win a triangle game at difficulty at least 20", GridType = GridType.Triangle, MinDifficulty = 20 },
            new Achievement { Id = 14, Name = "Win a triangle game at difficulty at least 20 under 30 seconds", GridType = GridType.Triangle, MinDifficulty = 20, MaxTimeSeconds = 30 },

            new Achievement { Id = 15, Name = "Win a squareTriHex game at size 5", GridType = GridType.SquareTriHex, MinSize = 5 },
            new Achievement { Id = 16, Name = "Win a squareTriHex game at size 10", GridType = GridType.SquareTriHex, MinSize = 10 },
            new Achievement { Id = 17, Name = "Win a squareTriHex game at difficulty at least 20", GridType = GridType.SquareTriHex, MinDifficulty = 20 },
            new Achievement { Id = 18, Name = "Win a squareTriHex game at difficulty at least 20 under 30 seconds", GridType = GridType.SquareTriHex, MinDifficulty = 20, MaxTimeSeconds = 30 }
        ];
    }

}
