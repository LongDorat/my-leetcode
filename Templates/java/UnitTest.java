import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class UnitTest {
    private final Solution solution = new Solution();

    @Test
    public void test() {
        int result = solution.run();
        // Add assertions here
        assertEquals(0, result);
    }
}
